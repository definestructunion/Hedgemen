namespace Hgm.API.Entities
{
    public sealed class UParty
    {
        public UPartyTypeInfo TypeInfo { get; private set; }

        private UParty()
        {

        }

        public UParty(UPartyArgs args)
        {
            TypeInfo = args.TypeInfo;
        }
    }

    public struct UPartyArgs
    {
        public UPartyTypeInfo TypeInfo;
    }
}