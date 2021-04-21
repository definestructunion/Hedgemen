using System.Runtime.Serialization;

namespace Hgm.Engine.Assets
{
    public enum AssetLoadType
    {
        [EnumMember(Value = "xnb")]
        Xnb,
        [EnumMember(Value = "music")]
        Music,
        [EnumMember(Value = "texture")]
        Texture,
        [EnumMember(Value = "sound")]
        Sound,
        [EnumMember(Value = "effect")]
        Effect,
        [EnumMember(Value = "font")]
        Font
    }
}