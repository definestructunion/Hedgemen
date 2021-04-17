namespace Hgm.API.Resources
{
	public delegate T TemplateCreate<out T, in TA>(TA args) where TA : struct;
	
	public class TypeInfoCreatable<T, TArgs> : TypeInfo<T> where TArgs : struct
	{
		public TemplateCreate<T, TArgs> TemplateCreator { get; set; } = e => default; 
		
		public T Create(TArgs args)
		{
			return TemplateCreator(args);
		}
	}
}