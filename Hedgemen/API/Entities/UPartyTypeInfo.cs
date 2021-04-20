using Hgm.API.Resources;

namespace Hgm.API.Entities
{
    public class UPartyTypeInfo : UTypeInfoCreatable<UParty, UPartyArgs>
    {
        public override UParty Create(UPartyArgs args)
        {
            args.TypeInfo = this;
            return new UParty(args);
        }
    }
}