using System.Collections.Generic;
using Hgm.Engine.Scenes.Nodes.Animators;
using Hgm.Input;
using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;

namespace Hgm.Engine.Scenes.Nodes
{
	public delegate void NodeEvent(Node node);
	
	public abstract class Node
	{
		public bool IsValid => attachedScene != null;
		
		public Node Parent { get; set; } = null;
	
		private Scene attachedScene;

		public Scene AttachedScene => attachedScene;

		protected List<Node> Children;

		protected NodeState NodeState { get; set; }

		public Color Color { get; set; } = Color.White;
		
		public NodeEvent OnHoverEvent { get; set; }
		
		public NodeEvent OnClickEvent { get; set; }
		
		public NodeEvent OnClickReleasedEvent { get; set; }
		
		public NodeEvent OnFocusEvent { get; set; }

		public NodeEvent OnDeFocusEvent { get; set; }
		
		public NodeEvent OnUpdate { get; set; }

		private Anchor anchor = Anchor.TopLeft;

		private Rectangle bounds = Rectangle.Empty;
		
		private Rectangle absoluteBounds = Rectangle.Empty;

		public bool Interactable { get; set; } = true;

		public bool Visible { get; set; } = true;

		public bool ChildrenAreVisible { get; set; } = true;

		private List<NodeAnimator> animators = new List<NodeAnimator>();

		public void AddAnimator(NodeAnimator animator)
		{
			animators.Add(animator);
			animator.AttachTo(this);
		}
		
		public IReadOnlyList<NodeAnimator> Animators => animators.AsReadOnly();

		public Anchor Anchor
		{
			get => anchor;
			set
			{
				anchor = value;
				UpdateBounds();
			}
		}
		
		public Rectangle Bounds
		{
			get => bounds;
			set
			{
				bounds = value;
				UpdateBounds();
			}
		}

		public Rectangle AbsoluteBounds => absoluteBounds;

		public Rectangle DrawBounds => AbsoluteBounds;

		public Rectangle InputBounds => AbsoluteBounds;
		
		public Vector2 Position
		{
			get => bounds.Position();
			set
			{
				bounds = bounds.PositionFrom(value);
				UpdateBounds();
			}
		}

		public Vector2 Size
		{
			get => bounds.Size();
			set
			{
				bounds = Bounds.SizeFrom(value);
				UpdateBounds();
			}
		}

		protected Node(Scene scene, Node parent)
		{
			parent?.AddChild(this);

			attachedScene = scene;
			Children = new List<Node>();
			
			OnHoverEvent = e => { };
			OnClickEvent = e => { };
			OnClickReleasedEvent = e => { };
			OnFocusEvent = e => { };
			OnDeFocusEvent = e => { };
			OnUpdate = e => { };
			
			bounds = new Rectangle(0, 0, 0, 0);
			absoluteBounds = CalculateBounds();
		}

		public virtual void Update(InputState inputState)
		{
			bool isHovering = IsHovering(inputState);
			bool isMouseDown = inputState.InputProvider.MouseButtonClick(MouseButtons.LeftButton);
			bool isMousePressed = inputState.InputProvider.MouseButtonClicked(MouseButtons.LeftButton);
			bool isMouseReleased = inputState.InputProvider.MouseButtonReleased(MouseButtons.LeftButton);

			DoUpdate(inputState);

			foreach (var animator in animators)
			{
				animator.Update(inputState);
			}

			animators.RemoveAll(e => !e.IsAttached);
			
			UpdateChildren(inputState);

			if (Interactable)
			{
				bool isThisTarget = inputState.TargetNode == this;

				NodeState = NodeState.Regular;
				if (isHovering && isThisTarget)
				{
					NodeState = NodeState.MouseHover;
				}

				if (isHovering && (isMouseDown || isMousePressed) && isThisTarget)
				{
					NodeState = NodeState.MouseDown;
				}

				if (NodeState == NodeState.MouseHover)
				{
					OnHover();
				}

				else if (NodeState == NodeState.MouseDown && isMouseReleased)
				{
					OnClickReleased();
				}
			
				else if (NodeState == NodeState.MouseDown)
				{
					OnClick();
				}

				
				
				if (isThisTarget && inputState.PreviousTargetNode != this)
				{
					OnFocus();
				}
			
				else if (!isThisTarget && inputState.PreviousTargetNode == this)
				{
					OnDeFocus();
				}
			}

			OnUpdate(this);
		}

		protected virtual void DoUpdate(InputState inputState)
		{
			
		}

		protected virtual void UpdateChildren(InputState inputState)
		{
			foreach (var child in Children)
			{
				child.Update(inputState);
			}
		}

		public virtual void Draw()
		{
			if (Visible)
				DoDraw();

			if(ChildrenAreVisible)
				DrawChildren();
		}

		protected virtual void DoDraw()
		{
			
		}

		protected virtual void DrawChildren()
		{
			foreach (var child in Children)
			{
				child.Draw();
			}
		}

		protected virtual bool IsHovering(InputState inputState)
		{
			var mousePosition = inputState.InputProvider.MousePosition;
			return InputBounds.Contains((int) mousePosition.X, (int) mousePosition.Y);
		}

		protected virtual void OnHover()
		{
			OnHoverEvent(this);
		}

		protected virtual void OnFocus()
		{
			OnFocusEvent(this);
		}

		protected virtual void OnDeFocus()
		{
			OnDeFocusEvent(this);
		}

		protected virtual void OnClick()
		{
			OnClickEvent(this);
		}

		protected virtual void OnClickReleased()
		{
			OnClickReleasedEvent(this);
		}

		protected virtual void UpdateBounds()
		{
			absoluteBounds = CalculateBounds();
			UpdateChildrenBounds();
		}

		public virtual Rectangle CalculateBounds()
		{
			var parentBounds = AttachedScene.Root.AbsoluteBounds;

			if(Parent != null) parentBounds = Parent.AbsoluteBounds;

			var relBounds = Bounds;
			var absBounds = Rectangle.Empty;
			absBounds.X = relBounds.X;
			absBounds.Y = relBounds.Y;

			absBounds.Width = relBounds.Width;
			absBounds.Height = relBounds.Height;

			switch (Anchor)
			{
				case Anchor.TopLeft:
					absBounds.X += parentBounds.Left;
					absBounds.Y += parentBounds.Top;
					break;
				case Anchor.Top:
					absBounds.X += parentBounds.Center.X - (relBounds.Width / 2);
					absBounds.Y += parentBounds.Top;
					break;
				case Anchor.TopRight:
					absBounds.X += parentBounds.Right - (relBounds.Width);
					absBounds.Y += parentBounds.Top;
					break;
				
				case Anchor.CenterLeft:
					absBounds.X += parentBounds.Left;
					absBounds.Y += parentBounds.Center.Y - (relBounds.Height / 2);
					break;
				case Anchor.Center: 
					absBounds.X += parentBounds.Center.X - (relBounds.Width / 2);
					absBounds.Y += parentBounds.Center.Y - (relBounds.Height / 2);
					break;
				case Anchor.CenterRight:
					absBounds.X += parentBounds.Right - (relBounds.Width);
					absBounds.Y += parentBounds.Center.Y - (relBounds.Height / 2);
					break;
				
				case Anchor.BottomLeft:
					absBounds.X += parentBounds.Left;
					absBounds.Y += parentBounds.Bottom - (relBounds.Height);
					break;
				case Anchor.Bottom:
					absBounds.X += parentBounds.Center.X - (relBounds.Width / 2);
					absBounds.Y += parentBounds.Bottom - (relBounds.Height);
					break;
				case Anchor.BottomRight:
					absBounds.X += parentBounds.Right - (relBounds.Width);
					absBounds.Y += parentBounds.Bottom - (relBounds.Height);
					break;
			}

			return absBounds;
		}

		protected virtual void UpdateChildrenBounds()
		{
			foreach (var child in Children)
			{
				child.UpdateBounds();
			}
		}

		public virtual Node AddChild(Node child)
		{
			Children.Add(child);
			child.Parent = this;
			return child;
		}

		public virtual void RemoveChild(Node child)
		{
			Children.Remove(child);
			child.Parent = null;
		}

		public virtual Node ScanForTargetNode(InputState inputState)
		{
			if (Children.Count > 0)
			{
				for (int i = Children.Count - 1; i >= 0; --i)
				{
					var child = Children[i];
					var targetNode = child.ScanForTargetNode(inputState);

					if (targetNode != null) return targetNode;
				}
			}

			if (Interactable && IsHovering(inputState)) return this;

			return null;
		}

		public void Discard()
		{
			foreach (var child in Children)
			{
				child.Discard();
			}
			
			OnDiscard();
			attachedScene = null;
			//Children.Clear();
		}

		protected virtual void OnDiscard()
		{
			
		}
	}
}