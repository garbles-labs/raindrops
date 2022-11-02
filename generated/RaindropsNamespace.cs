using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Solana.Unity;
using Solana.Unity.Programs.Abstract;
using Solana.Unity.Programs.Utilities;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Core.Sockets;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;
using RaindropsNamespace;
using RaindropsNamespace.Program;
using RaindropsNamespace.Errors;
using RaindropsNamespace.Accounts;
using RaindropsNamespace.Types;

namespace RaindropsNamespace
{
    namespace Accounts
    {
        public partial class NamespaceGatekeeper
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 15014388035552266534UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{38, 201, 247, 98, 94, 210, 93, 208};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "7VJUJeAbe9D";
            public byte Bump { get; set; }

            public PublicKey Namespace { get; set; }

            public ArtifactFilter[] ArtifactFilters { get; set; }

            public static NamespaceGatekeeper Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                NamespaceGatekeeper result = new NamespaceGatekeeper();
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.Namespace = _data.GetPubKey(offset);
                offset += 32;
                int resultArtifactFiltersLength = (int)_data.GetU32(offset);
                offset += 4;
                result.ArtifactFilters = new ArtifactFilter[resultArtifactFiltersLength];
                for (uint resultArtifactFiltersIdx = 0; resultArtifactFiltersIdx < resultArtifactFiltersLength; resultArtifactFiltersIdx++)
                {
                    offset += ArtifactFilter.Deserialize(_data, offset, out var resultArtifactFiltersresultArtifactFiltersIdx);
                    result.ArtifactFilters[resultArtifactFiltersIdx] = resultArtifactFiltersresultArtifactFiltersIdx;
                }

                return result;
            }
        }

        public partial class Namespace
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 7773035093979641641UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{41, 55, 77, 19, 60, 94, 223, 107};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "7tr8P5DE6wt";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey MasterEdition { get; set; }

            public string Uuid { get; set; }

            public string PrettyName { get; set; }

            public ulong ArtifactsAdded { get; set; }

            public ulong MaxPages { get; set; }

            public ulong[] FullPages { get; set; }

            public ulong ArtifactsCached { get; set; }

            public PermissivenessSettings PermissivenessSettings { get; set; }

            public byte Bump { get; set; }

            public PublicKey[] WhitelistedStakingMints { get; set; }

            public PublicKey Gatekeeper { get; set; }

            public PublicKey PaymentMint { get; set; }

            public PublicKey PaymentVault { get; set; }

            public ulong? PaymentAmount { get; set; }

            public static Namespace Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                Namespace result = new Namespace();
                if (_data.GetBool(offset++))
                {
                    int resultNamespacesLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.Namespaces = new NamespaceAndIndex[resultNamespacesLength];
                    for (uint resultNamespacesIdx = 0; resultNamespacesIdx < resultNamespacesLength; resultNamespacesIdx++)
                    {
                        offset += NamespaceAndIndex.Deserialize(_data, offset, out var resultNamespacesresultNamespacesIdx);
                        result.Namespaces[resultNamespacesIdx] = resultNamespacesresultNamespacesIdx;
                    }
                }

                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                result.Metadata = _data.GetPubKey(offset);
                offset += 32;
                result.MasterEdition = _data.GetPubKey(offset);
                offset += 32;
                offset += _data.GetBorshString(offset, out var resultUuid);
                result.Uuid = resultUuid;
                offset += _data.GetBorshString(offset, out var resultPrettyName);
                result.PrettyName = resultPrettyName;
                result.ArtifactsAdded = _data.GetU64(offset);
                offset += 8;
                result.MaxPages = _data.GetU64(offset);
                offset += 8;
                int resultFullPagesLength = (int)_data.GetU32(offset);
                offset += 4;
                result.FullPages = new ulong[resultFullPagesLength];
                for (uint resultFullPagesIdx = 0; resultFullPagesIdx < resultFullPagesLength; resultFullPagesIdx++)
                {
                    result.FullPages[resultFullPagesIdx] = _data.GetU64(offset);
                    offset += 8;
                }

                result.ArtifactsCached = _data.GetU64(offset);
                offset += 8;
                offset += PermissivenessSettings.Deserialize(_data, offset, out var resultPermissivenessSettings);
                result.PermissivenessSettings = resultPermissivenessSettings;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                int resultWhitelistedStakingMintsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.WhitelistedStakingMints = new PublicKey[resultWhitelistedStakingMintsLength];
                for (uint resultWhitelistedStakingMintsIdx = 0; resultWhitelistedStakingMintsIdx < resultWhitelistedStakingMintsLength; resultWhitelistedStakingMintsIdx++)
                {
                    result.WhitelistedStakingMints[resultWhitelistedStakingMintsIdx] = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Gatekeeper = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.PaymentMint = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.PaymentVault = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.PaymentAmount = _data.GetU64(offset);
                    offset += 8;
                }

                return result;
            }
        }

        public partial class NamespaceIndex
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 2574489996396871099UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{187, 61, 8, 241, 2, 109, 186, 35};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "YKSwNHrxRDc";
            public PublicKey Namespace { get; set; }

            public byte Bump { get; set; }

            public ulong Page { get; set; }

            public PublicKey[] Caches { get; set; }

            public static NamespaceIndex Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                NamespaceIndex result = new NamespaceIndex();
                result.Namespace = _data.GetPubKey(offset);
                offset += 32;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.Page = _data.GetU64(offset);
                offset += 8;
                int resultCachesLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Caches = new PublicKey[resultCachesLength];
                for (uint resultCachesIdx = 0; resultCachesIdx < resultCachesLength; resultCachesIdx++)
                {
                    result.Caches[resultCachesIdx] = _data.GetPubKey(offset);
                    offset += 32;
                }

                return result;
            }
        }
    }

    namespace Errors
    {
        public enum RaindropsNamespaceErrorKind : uint
        {
            IncorrectOwner = 6000U,
            Uninitialized = 6001U,
            MintMismatch = 6002U,
            TokenTransferFailed = 6003U,
            NumericalOverflowError = 6004U,
            TokenMintToFailed = 6005U,
            TokenBurnFailed = 6006U,
            DerivedKeyInvalid = 6007U,
            UUIDTooLong = 6008U,
            PrettyNameTooLong = 6009U,
            WhitelistStakeListTooLong = 6010U,
            MetadataDoesntExist = 6011U,
            EditionDoesntExist = 6012U,
            PreviousIndexNotFull = 6013U,
            IndexFull = 6014U,
            CanOnlyCacheValidRaindropsObjects = 6015U,
            ArtifactLacksNamespace = 6016U,
            ArtifactNotPartOfNamespace = 6017U,
            CannotJoinNamespace = 6018U,
            CannotLeaveNamespace = 6019U,
            ArtifactStillCached = 6020U,
            CacheFull = 6021U,
            CannotUncacheArtifact = 6022U,
            CannotCacheArtifact = 6023U,
            DesiredNamespacesNone = 6024U,
            InvalidRemainingAccounts = 6025U
        }
    }

    namespace Types
    {
        public partial class InitializeNamespaceArgs
        {
            public ulong DesiredNamespaceArraySize { get; set; }

            public string Uuid { get; set; }

            public string PrettyName { get; set; }

            public PermissivenessSettings PermissivenessSettings { get; set; }

            public PublicKey[] WhitelistedStakingMints { get; set; }

            public ulong? PaymentAmount { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(DesiredNamespaceArraySize, offset);
                offset += 8;
                offset += _data.WriteBorshString(Uuid, offset);
                offset += _data.WriteBorshString(PrettyName, offset);
                offset += PermissivenessSettings.Serialize(_data, offset);
                _data.WriteS32(WhitelistedStakingMints.Length, offset);
                offset += 4;
                foreach (var whitelistedStakingMintsElement in WhitelistedStakingMints)
                {
                    _data.WritePubKey(whitelistedStakingMintsElement, offset);
                    offset += 32;
                }

                if (PaymentAmount != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(PaymentAmount.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out InitializeNamespaceArgs result)
            {
                int offset = initialOffset;
                result = new InitializeNamespaceArgs();
                result.DesiredNamespaceArraySize = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultUuid);
                result.Uuid = resultUuid;
                offset += _data.GetBorshString(offset, out var resultPrettyName);
                result.PrettyName = resultPrettyName;
                offset += PermissivenessSettings.Deserialize(_data, offset, out var resultPermissivenessSettings);
                result.PermissivenessSettings = resultPermissivenessSettings;
                int resultWhitelistedStakingMintsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.WhitelistedStakingMints = new PublicKey[resultWhitelistedStakingMintsLength];
                for (uint resultWhitelistedStakingMintsIdx = 0; resultWhitelistedStakingMintsIdx < resultWhitelistedStakingMintsLength; resultWhitelistedStakingMintsIdx++)
                {
                    result.WhitelistedStakingMints[resultWhitelistedStakingMintsIdx] = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.PaymentAmount = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }
        }

        public partial class UpdateNamespaceArgs
        {
            public string PrettyName { get; set; }

            public PermissivenessSettings PermissivenessSettings { get; set; }

            public PublicKey[] WhitelistedStakingMints { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (PrettyName != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += _data.WriteBorshString(PrettyName, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (PermissivenessSettings != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += PermissivenessSettings.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (WhitelistedStakingMints != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(WhitelistedStakingMints.Length, offset);
                    offset += 4;
                    foreach (var whitelistedStakingMintsElement in WhitelistedStakingMints)
                    {
                        _data.WritePubKey(whitelistedStakingMintsElement, offset);
                        offset += 32;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateNamespaceArgs result)
            {
                int offset = initialOffset;
                result = new UpdateNamespaceArgs();
                if (_data.GetBool(offset++))
                {
                    offset += _data.GetBorshString(offset, out var resultPrettyName);
                    result.PrettyName = resultPrettyName;
                }

                if (_data.GetBool(offset++))
                {
                    offset += PermissivenessSettings.Deserialize(_data, offset, out var resultPermissivenessSettings);
                    result.PermissivenessSettings = resultPermissivenessSettings;
                }

                if (_data.GetBool(offset++))
                {
                    int resultWhitelistedStakingMintsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.WhitelistedStakingMints = new PublicKey[resultWhitelistedStakingMintsLength];
                    for (uint resultWhitelistedStakingMintsIdx = 0; resultWhitelistedStakingMintsIdx < resultWhitelistedStakingMintsLength; resultWhitelistedStakingMintsIdx++)
                    {
                        result.WhitelistedStakingMints[resultWhitelistedStakingMintsIdx] = _data.GetPubKey(offset);
                        offset += 32;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class CacheArtifactArgs
        {
            public ulong Page { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Page, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CacheArtifactArgs result)
            {
                int offset = initialOffset;
                result = new CacheArtifactArgs();
                result.Page = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class UncacheArtifactArgs
        {
            public ulong Page { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Page, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UncacheArtifactArgs result)
            {
                int offset = initialOffset;
                result = new UncacheArtifactArgs();
                result.Page = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class ArtifactFilter
        {
            public Filter Filter { get; set; }

            public ArtifactType TokenType { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += Filter.Serialize(_data, offset);
                _data.WriteU8((byte)TokenType, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ArtifactFilter result)
            {
                int offset = initialOffset;
                result = new ArtifactFilter();
                offset += Filter.Deserialize(_data, offset, out var resultFilter);
                result.Filter = resultFilter;
                result.TokenType = (ArtifactType)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class PermissivenessSettings
        {
            public Permissiveness NamespacePermissiveness { get; set; }

            public Permissiveness ItemPermissiveness { get; set; }

            public Permissiveness PlayerPermissiveness { get; set; }

            public Permissiveness MatchPermissiveness { get; set; }

            public Permissiveness MissionPermissiveness { get; set; }

            public Permissiveness CachePermissiveness { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)NamespacePermissiveness, offset);
                offset += 1;
                _data.WriteU8((byte)ItemPermissiveness, offset);
                offset += 1;
                _data.WriteU8((byte)PlayerPermissiveness, offset);
                offset += 1;
                _data.WriteU8((byte)MatchPermissiveness, offset);
                offset += 1;
                _data.WriteU8((byte)MissionPermissiveness, offset);
                offset += 1;
                _data.WriteU8((byte)CachePermissiveness, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PermissivenessSettings result)
            {
                int offset = initialOffset;
                result = new PermissivenessSettings();
                result.NamespacePermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                result.ItemPermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                result.PlayerPermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                result.MatchPermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                result.MissionPermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                result.CachePermissiveness = (Permissiveness)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class NamespaceAndIndex
        {
            public PublicKey Namespace { get; set; }

            public ulong? Index { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Namespace, offset);
                offset += 32;
                if (Index != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(Index.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out NamespaceAndIndex result)
            {
                int offset = initialOffset;
                result = new NamespaceAndIndex();
                result.Namespace = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.Index = _data.GetU64(offset);
                    offset += 8;
                }

                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class ValidationArgs
        {
            public ulong ExtraIdentifier { get; set; }

            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public ulong Amount { get; set; }

            public byte? UsagePermissivenessToUse { get; set; }

            public ushort UsageIndex { get; set; }

            public byte? UsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ExtraIdentifier, offset);
                offset += 8;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (UsagePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8(UsagePermissivenessToUse.Value, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                if (UsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8(UsageInfo.Value, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ValidationArgs result)
            {
                int offset = initialOffset;
                result = new ValidationArgs();
                result.ExtraIdentifier = _data.GetU64(offset);
                offset += 8;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.UsagePermissivenessToUse = _data.GetU8(offset);
                    offset += 1;
                }

                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.UsageInfo = _data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class Callback
        {
            public PublicKey Key { get; set; }

            public ulong Code { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Key, offset);
                offset += 32;
                _data.WriteU64(Code, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Callback result)
            {
                int offset = initialOffset;
                result = new Callback();
                result.Key = _data.GetPubKey(offset);
                offset += 32;
                result.Code = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class TokenValidation
        {
            public TokenValidationFilter Filter { get; set; }

            public bool IsBlacklist { get; set; }

            public Callback Validation { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += Filter.Serialize(_data, offset);
                _data.WriteBool(IsBlacklist, offset);
                offset += 1;
                if (Validation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Validation.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TokenValidation result)
            {
                int offset = initialOffset;
                result = new TokenValidation();
                offset += TokenValidationFilter.Deserialize(_data, offset, out var resultFilter);
                result.Filter = resultFilter;
                result.IsBlacklist = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    offset += Callback.Deserialize(_data, offset, out var resultValidation);
                    result.Validation = resultValidation;
                }

                return offset - initialOffset;
            }
        }

        public partial class MatchValidationArgs
        {
            public ulong ExtraIdentifier { get; set; }

            public TokenValidation TokenValidation { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ExtraIdentifier, offset);
                offset += 8;
                offset += TokenValidation.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out MatchValidationArgs result)
            {
                int offset = initialOffset;
                result = new MatchValidationArgs();
                result.ExtraIdentifier = _data.GetU64(offset);
                offset += 8;
                offset += TokenValidation.Deserialize(_data, offset, out var resultTokenValidation);
                result.TokenValidation = resultTokenValidation;
                return offset - initialOffset;
            }
        }

        public enum Permissiveness : byte
        {
            All,
            Whitelist,
            Blacklist,
            Namespace
        }

        public enum ArtifactType : byte
        {
            Player,
            Item,
            Mission,
            Namespace
        }

        public enum FilterType : byte
        {
            Namespace,
            Key
        }

        public partial class NamespaceType
        {
            public PublicKey[] Namespaces { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out NamespaceType result)
            {
                int offset = initialOffset;
                result = new NamespaceType();
                int resultNamespacesLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Namespaces = new PublicKey[resultNamespacesLength];
                for (uint resultNamespacesIdx = 0; resultNamespacesIdx < resultNamespacesLength; resultNamespacesIdx++)
                {
                    result.Namespaces[resultNamespacesIdx] = _data.GetPubKey(offset);
                    offset += 32;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(Namespaces.Length, offset);
                offset += 4;
                foreach (var namespacesElement in Namespaces)
                {
                    _data.WritePubKey(namespacesElement, offset);
                    offset += 32;
                }

                return offset - initialOffset;
            }
        }

        public partial class KeyType
        {
            public PublicKey Key { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out KeyType result)
            {
                int offset = initialOffset;
                result = new KeyType();
                result.Key = _data.GetPubKey(offset);
                offset += 32;
                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                result.Metadata = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.Edition = _data.GetPubKey(offset);
                    offset += 32;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Key, offset);
                offset += 32;
                _data.WritePubKey(Mint, offset);
                offset += 32;
                _data.WritePubKey(Metadata, offset);
                offset += 32;
                if (Edition != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WritePubKey(Edition, offset);
                    offset += 32;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class Filter
        {
            public NamespaceType NamespaceValue { get; set; }

            public KeyType KeyValue { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Type, offset);
                offset += 1;
                switch (Type)
                {
                    case FilterType.Namespace:
                        offset += NamespaceValue.Serialize(_data, offset);
                        break;
                    case FilterType.Key:
                        offset += KeyValue.Serialize(_data, offset);
                        break;
                }

                return offset - initialOffset;
            }

            public FilterType Type { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Filter result)
            {
                int offset = initialOffset;
                result = new Filter();
                result.Type = (FilterType)_data.GetU8(offset);
                offset += 1;
                switch (result.Type)
                {
                    case FilterType.Namespace:
                    {
                        NamespaceType tmpNamespaceValue = new NamespaceType();
                        offset += NamespaceType.Deserialize(_data, offset, out tmpNamespaceValue);
                        result.NamespaceValue = tmpNamespaceValue;
                        break;
                    }

                    case FilterType.Key:
                    {
                        KeyType tmpKeyValue = new KeyType();
                        offset += KeyType.Deserialize(_data, offset, out tmpKeyValue);
                        result.KeyValue = tmpKeyValue;
                        break;
                    }
                }

                return offset - initialOffset;
            }
        }

        public enum InheritanceState : byte
        {
            NotInherited,
            Inherited,
            Overridden
        }

        public enum TokenValidationFilterType : byte
        {
            None,
            All,
            Namespace,
            Parent,
            Mint
        }

        public partial class NamespaceType
        {
            public PublicKey Namespace { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out NamespaceType result)
            {
                int offset = initialOffset;
                result = new NamespaceType();
                result.Namespace = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Namespace, offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class ParentType
        {
            public PublicKey Key { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ParentType result)
            {
                int offset = initialOffset;
                result = new ParentType();
                result.Key = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Key, offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class MintType
        {
            public PublicKey Mint { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out MintType result)
            {
                int offset = initialOffset;
                result = new MintType();
                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Mint, offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class TokenValidationFilter
        {
            public NamespaceType NamespaceValue { get; set; }

            public ParentType ParentValue { get; set; }

            public MintType MintValue { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Type, offset);
                offset += 1;
                switch (Type)
                {
                    case TokenValidationFilterType.Namespace:
                        offset += NamespaceValue.Serialize(_data, offset);
                        break;
                    case TokenValidationFilterType.Parent:
                        offset += ParentValue.Serialize(_data, offset);
                        break;
                    case TokenValidationFilterType.Mint:
                        offset += MintValue.Serialize(_data, offset);
                        break;
                }

                return offset - initialOffset;
            }

            public TokenValidationFilterType Type { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TokenValidationFilter result)
            {
                int offset = initialOffset;
                result = new TokenValidationFilter();
                result.Type = (TokenValidationFilterType)_data.GetU8(offset);
                offset += 1;
                switch (result.Type)
                {
                    case TokenValidationFilterType.Namespace:
                    {
                        NamespaceType tmpNamespaceValue = new NamespaceType();
                        offset += NamespaceType.Deserialize(_data, offset, out tmpNamespaceValue);
                        result.NamespaceValue = tmpNamespaceValue;
                        break;
                    }

                    case TokenValidationFilterType.Parent:
                    {
                        ParentType tmpParentValue = new ParentType();
                        offset += ParentType.Deserialize(_data, offset, out tmpParentValue);
                        result.ParentValue = tmpParentValue;
                        break;
                    }

                    case TokenValidationFilterType.Mint:
                    {
                        MintType tmpMintValue = new MintType();
                        offset += MintType.Deserialize(_data, offset, out tmpMintValue);
                        result.MintValue = tmpMintValue;
                        break;
                    }
                }

                return offset - initialOffset;
            }
        }
    }

    public partial class RaindropsNamespaceClient : TransactionalBaseClient<RaindropsNamespaceErrorKind>
    {
        public RaindropsNamespaceClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceGatekeeper>>> GetNamespaceGatekeepersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = NamespaceGatekeeper.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceGatekeeper>>(res);
            List<NamespaceGatekeeper> resultingAccounts = new List<NamespaceGatekeeper>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => NamespaceGatekeeper.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceGatekeeper>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Namespace>>> GetNamespacesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = Namespace.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Namespace>>(res);
            List<Namespace> resultingAccounts = new List<Namespace>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Namespace.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Namespace>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceIndex>>> GetNamespaceIndexsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = NamespaceIndex.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceIndex>>(res);
            List<NamespaceIndex> resultingAccounts = new List<NamespaceIndex>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => NamespaceIndex.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<NamespaceIndex>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceGatekeeper>> GetNamespaceGatekeeperAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceGatekeeper>(res);
            var resultingAccount = NamespaceGatekeeper.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceGatekeeper>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<Namespace>> GetNamespaceAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<Namespace>(res);
            var resultingAccount = Namespace.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<Namespace>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceIndex>> GetNamespaceIndexAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceIndex>(res);
            var resultingAccount = NamespaceIndex.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<NamespaceIndex>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeNamespaceGatekeeperAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, NamespaceGatekeeper> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                NamespaceGatekeeper parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = NamespaceGatekeeper.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeNamespaceAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, Namespace> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Namespace parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Namespace.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeNamespaceIndexAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, NamespaceIndex> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                NamespaceIndex parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = NamespaceIndex.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendInitializeNamespaceAsync(InitializeNamespaceAccounts accounts, InitializeNamespaceArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.InitializeNamespace(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateNamespaceAsync(UpdateNamespaceAccounts accounts, UpdateNamespaceArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.UpdateNamespace(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCacheArtifactAsync(CacheArtifactAccounts accounts, CacheArtifactArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.CacheArtifact(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUncacheArtifactAsync(UncacheArtifactAccounts accounts, UncacheArtifactArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.UncacheArtifact(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCreateNamespaceGatekeeperAsync(CreateNamespaceGatekeeperAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.CreateNamespaceGatekeeper(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendAddToNamespaceGatekeeperAsync(AddToNamespaceGatekeeperAccounts accounts, ArtifactFilter artifactFilter, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.AddToNamespaceGatekeeper(accounts, artifactFilter, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRemoveFromNamespaceGatekeeperAsync(RemoveFromNamespaceGatekeeperAccounts accounts, ArtifactFilter artifactFilter, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.RemoveFromNamespaceGatekeeper(accounts, artifactFilter, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendLeaveNamespaceAsync(LeaveNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.LeaveNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendJoinNamespaceAsync(JoinNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.JoinNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendItemValidationAsync(ItemValidationAccounts accounts, ValidationArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.ItemValidation(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMatchValidationAsync(MatchValidationAccounts accounts, MatchValidationArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsNamespaceProgram.MatchValidation(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<RaindropsNamespaceErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<RaindropsNamespaceErrorKind>>{{6000U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.IncorrectOwner, "Account does not have correct owner!")}, {6001U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.Uninitialized, "Account is not initialized!")}, {6002U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.MintMismatch, "Mint Mismatch!")}, {6003U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.TokenTransferFailed, "Token transfer failed")}, {6004U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.NumericalOverflowError, "Numerical overflow error")}, {6005U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.TokenMintToFailed, "Token mint to failed")}, {6006U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.TokenBurnFailed, "TokenBurnFailed")}, {6007U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.DerivedKeyInvalid, "Derived key is invalid")}, {6008U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.UUIDTooLong, "UUID too long, 6 char max")}, {6009U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.PrettyNameTooLong, "Pretty name too long, 32 char max")}, {6010U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.WhitelistStakeListTooLong, "Whitelist stake list too long, 5 max")}, {6011U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.MetadataDoesntExist, "Metadata doesnt exist")}, {6012U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.EditionDoesntExist, "Edition doesnt exist")}, {6013U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.PreviousIndexNotFull, "The previous index is not full yet, so you cannot make a new one")}, {6014U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.IndexFull, "Index is full")}, {6015U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CanOnlyCacheValidRaindropsObjects, "Can only cache valid raindrops artifacts (players, items, matches)")}, {6016U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.ArtifactLacksNamespace, "Artifact lacks namespace!")}, {6017U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.ArtifactNotPartOfNamespace, "Artifact not part of namespace!")}, {6018U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CannotJoinNamespace, "You do not have permissions to join this namespace")}, {6019U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CannotLeaveNamespace, "Error leaving namespace")}, {6020U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.ArtifactStillCached, "You cannot remove an artifact from a namespace while it is still cached there. Uncache it first.")}, {6021U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CacheFull, "Artifact Cache full")}, {6022U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CannotUncacheArtifact, "Cannot Uncache Artifact")}, {6023U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.CannotCacheArtifact, "Cannot Cache Artifact")}, {6024U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.DesiredNamespacesNone, "Artifact not configured for namespaces")}, {6025U, new ProgramError<RaindropsNamespaceErrorKind>(RaindropsNamespaceErrorKind.InvalidRemainingAccounts, "Invalid Remaining Accounts")}, };
        }
    }

    namespace Program
    {
        public class InitializeNamespaceAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey MasterEdition { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class UpdateNamespaceAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey TokenHolder { get; set; }
        }

        public class CacheArtifactAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey Index { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey TokenHolder { get; set; }

            public PublicKey Instructions { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey RaindropsProgram { get; set; }
        }

        public class UncacheArtifactAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey Index { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey TokenHolder { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Instructions { get; set; }

            public PublicKey RaindropsProgram { get; set; }
        }

        public class CreateNamespaceGatekeeperAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey NamespaceGatekeeper { get; set; }

            public PublicKey TokenHolder { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class AddToNamespaceGatekeeperAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey NamespaceGatekeeper { get; set; }

            public PublicKey TokenHolder { get; set; }
        }

        public class RemoveFromNamespaceGatekeeperAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey NamespaceGatekeeper { get; set; }

            public PublicKey TokenHolder { get; set; }
        }

        public class LeaveNamespaceAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey NamespaceGatekeeper { get; set; }

            public PublicKey TokenHolder { get; set; }

            public PublicKey Instructions { get; set; }

            public PublicKey RaindropsProgram { get; set; }
        }

        public class JoinNamespaceAccounts
        {
            public PublicKey Namespace { get; set; }

            public PublicKey NamespaceToken { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey NamespaceGatekeeper { get; set; }

            public PublicKey TokenHolder { get; set; }

            public PublicKey RainTokenMint { get; set; }

            public PublicKey RainTokenVault { get; set; }

            public PublicKey RainPayerAta { get; set; }

            public PublicKey Instructions { get; set; }

            public PublicKey RaindropsProgram { get; set; }

            public PublicKey TokenProgram { get; set; }
        }

        public class ItemValidationAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemAccount { get; set; }
        }

        public class MatchValidationAccounts
        {
            public PublicKey SourceItemOrPlayerPda { get; set; }

            public PublicKey Mint { get; set; }
        }

        public static class RaindropsNamespaceProgram
        {
            public static Solana.Unity.Rpc.Models.TransactionInstruction InitializeNamespace(InitializeNamespaceAccounts accounts, InitializeNamespaceArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Mint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.MasterEdition, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13114610296831753167UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateNamespace(UpdateNamespaceAccounts accounts, UpdateNamespaceArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenHolder, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(18384917363692628797UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction CacheArtifact(CacheArtifactAccounts accounts, CacheArtifactArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Index, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenHolder, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RaindropsProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6030556068184935835UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UncacheArtifact(UncacheArtifactAccounts accounts, UncacheArtifactArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Index, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenHolder, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RaindropsProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(8485849743486066044UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction CreateNamespaceGatekeeper(CreateNamespaceGatekeeperAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NamespaceGatekeeper, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenHolder, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(2409365804399931506UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction AddToNamespaceGatekeeper(AddToNamespaceGatekeeperAccounts accounts, ArtifactFilter artifactFilter, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NamespaceGatekeeper, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenHolder, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(17389851975403884628UL, offset);
                offset += 8;
                offset += artifactFilter.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction RemoveFromNamespaceGatekeeper(RemoveFromNamespaceGatekeeperAccounts accounts, ArtifactFilter artifactFilter, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NamespaceGatekeeper, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenHolder, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(7556276586811303106UL, offset);
                offset += 8;
                offset += artifactFilter.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction LeaveNamespace(LeaveNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceGatekeeper, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenHolder, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RaindropsProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6061411801043533605UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction JoinNamespace(JoinNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NamespaceGatekeeper, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenHolder, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RainTokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.RainTokenVault, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.RainPayerAta, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RaindropsProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(5790627020061890201UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ItemValidation(ItemValidationAccounts accounts, ValidationArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemAccount, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11455820392563473120UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction MatchValidation(MatchValidationAccounts accounts, MatchValidationArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SourceItemOrPlayerPda, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Mint, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(2070599071839712134UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }
}