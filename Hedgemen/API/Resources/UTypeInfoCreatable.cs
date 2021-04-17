namespace Hgm.API.Resources
{
	public abstract class UTypeInfoCreatable<T, TA> : UTypeInfo<T>
	{
		protected UTypeInfoCreatable()
		{
			
		}

		public abstract T Create(TA args);
	}
}