namespace Hgm.Engine.Scenes.Nodes.Animators
{
    public abstract class NodeAnimator
    {
        private Node node;

        protected Node Node => node;
        
        protected NodeAnimator()
        {
            
        }

        public bool IsAttached => node != null;

        public void AttachTo(Node node)
        {
            if (!ShouldAttachTo(node)) return;

            this.node = node;
            OnAttached();
        }

        public void Detach()
        {
            OnDetach();
            node = null;
        }

        protected virtual bool ShouldAttachTo(Node node)
        {
            return true;
        }

        protected virtual void OnAttached()
        {
            
        }

        protected virtual void OnDetach()
        {
            
        }

        public void Update(InputState inputState)
        {
            if (ShouldDetatch())
            {
                Detach();
            }
            
            OnUpdate(inputState);
        }

        public void Draw()
        {
            OnDraw();
        }

        protected virtual void OnDraw()
        {
            
        }

        protected virtual void OnUpdate(InputState inputState)
        {
            
        }

        protected virtual bool ShouldDetatch()
        {
            return false;
        }
    }
}