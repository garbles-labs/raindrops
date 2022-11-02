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
using RaindropsItem;
using RaindropsItem.Program;
using RaindropsItem.Errors;
using RaindropsItem.Accounts;
using RaindropsItem.Types;

namespace RaindropsItem
{
    namespace Accounts
    {
        public partial class ItemClass
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 10892138324794791100UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{188, 212, 207, 237, 24, 166, 40, 151};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "YauD2f6ReiA";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public PublicKey Parent { get; set; }

            public PublicKey Mint { get; set; }

            public ulong? Index { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public byte Bump { get; set; }

            public ulong ExistingChildren { get; set; }

            public static ItemClass Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                ItemClass result = new ItemClass();
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

                if (_data.GetBool(offset++))
                {
                    result.Parent = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Mint = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Index = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.Metadata = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Edition = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.ExistingChildren = _data.GetU64(offset);
                offset += 8;
                return result;
            }
        }

        public partial class ItemEscrow
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 1907748379046259825UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{113, 44, 225, 5, 11, 175, 121, 26};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "KvwembkVf5B";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public byte Bump { get; set; }

            public bool Deactivated { get; set; }

            public ulong Step { get; set; }

            public ulong? TimeToBuild { get; set; }

            public ulong? BuildBegan { get; set; }

            public static ItemEscrow Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                ItemEscrow result = new ItemEscrow();
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

                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.Deactivated = _data.GetBool(offset);
                offset += 1;
                result.Step = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.TimeToBuild = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.BuildBegan = _data.GetU64(offset);
                    offset += 8;
                }

                return result;
            }
        }

        public partial class CraftItemCounter
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 13488449702750853106UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{242, 23, 48, 47, 138, 153, 48, 187};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "hVakR5dYbbg";
            public ulong AmountLoaded { get; set; }

            public static CraftItemCounter Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                CraftItemCounter result = new CraftItemCounter();
                result.AmountLoaded = _data.GetU64(offset);
                offset += 8;
                return result;
            }
        }

        public partial class ItemActivationMarker
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 11067442934945591589UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{37, 177, 86, 37, 182, 116, 151, 153};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "7JffU3wJdRJ";
            public byte Bump { get; set; }

            public bool ValidForUse { get; set; }

            public ulong Amount { get; set; }

            public ulong UnixTimestamp { get; set; }

            public ItemActivationMarkerProofCounter ProofCounter { get; set; }

            public PublicKey Target { get; set; }

            public static ItemActivationMarker Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                ItemActivationMarker result = new ItemActivationMarker();
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.ValidForUse = _data.GetBool(offset);
                offset += 1;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.UnixTimestamp = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    offset += ItemActivationMarkerProofCounter.Deserialize(_data, offset, out var resultProofCounter);
                    result.ProofCounter = resultProofCounter;
                }

                if (_data.GetBool(offset++))
                {
                    result.Target = _data.GetPubKey(offset);
                    offset += 32;
                }

                return result;
            }
        }

        public partial class Item
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 15588926747572411740UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{92, 157, 163, 130, 72, 254, 86, 216};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "GVVN9RX2SX1";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public byte Padding { get; set; }

            public PublicKey Parent { get; set; }

            public ulong ClassIndex { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public byte Bump { get; set; }

            public ulong TokensStaked { get; set; }

            public ItemData Data { get; set; }

            public static Item Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                Item result = new Item();
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

                result.Padding = _data.GetU8(offset);
                offset += 1;
                result.Parent = _data.GetPubKey(offset);
                offset += 32;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.Mint = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Metadata = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Edition = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.TokensStaked = _data.GetU64(offset);
                offset += 8;
                offset += ItemData.Deserialize(_data, offset, out var resultData);
                result.Data = resultData;
                return result;
            }
        }
    }

    namespace Errors
    {
        public enum RaindropsItemErrorKind : uint
        {
            IncorrectOwner = 6000U,
            Uninitialized = 6001U,
            MintMismatch = 6002U,
            TokenTransferFailed = 6003U,
            NumericalOverflowError = 6004U,
            TokenMintToFailed = 6005U,
            TokenBurnFailed = 6006U,
            DerivedKeyInvalid = 6007U,
            MustSpecifyPermissivenessType = 6008U,
            PermissivenessNotFound = 6009U,
            PublicKeyMismatch = 6010U,
            InsufficientBalance = 6011U,
            MetadataDoesntExist = 6012U,
            EditionDoesntExist = 6013U,
            NoParentPresent = 6014U,
            ExpectedParent = 6015U,
            InvalidMintAuthority = 6016U,
            NotMintAuthority = 6017U,
            CannotMakeZero = 6018U,
            MustBeHolderToBuild = 6019U,
            InvalidConfigForFungibleMints = 6020U,
            MissingMerkleInfo = 6021U,
            InvalidProof = 6022U,
            ItemReadyForCompletion = 6023U,
            MustUseMerkleOrComponentList = 6024U,
            MustUseMerkleOrUsageState = 6025U,
            UnableToFindValidCooldownState = 6026U,
            BalanceNeedsToBeZero = 6027U,
            NotPartOfComponentScope = 6028U,
            TimeToBuildMismatch = 6029U,
            StakingMintNotWhitelisted = 6030U,
            BuildPhaseNotStarted = 6031U,
            BuildPhaseNotFinished = 6032U,
            DeactivatedItemEscrow = 6033U,
            BuildPhaseAlreadyStarted = 6034U,
            StillMissingComponents = 6035U,
            ChildrenStillExist = 6036U,
            UnstakeTokensFirst = 6037U,
            AlreadyDeactivated = 6038U,
            NotDeactivated = 6039U,
            NotEmptied = 6040U,
            GivingTooMuch = 6041U,
            MustProvideUsageIndex = 6042U,
            CannotUseItemWithoutUsageOrMerkle = 6043U,
            MaxUsesReached = 6044U,
            CooldownNotOver = 6045U,
            CannotUseWearable = 6046U,
            UsageIndexMismatch = 6047U,
            ProvingNewStateNotRequired = 6048U,
            MustSubmitStatesInOrder = 6049U,
            ItemActivationNotValidYet = 6050U,
            WarmupNotFinished = 6051U,
            MustBeChild = 6052U,
            MustUseRealScope = 6053U,
            CraftClassIndexMismatch = 6054U,
            MustBeGreaterThanZero = 6055U,
            AtaShouldNotHaveDelegate = 6056U,
            ReinitializationDetected = 6057U,
            FailedToJoinNamespace = 6058U,
            FailedToLeaveNamespace = 6059U,
            FailedToCache = 6060U,
            FailedToUncache = 6061U,
            AlreadyCached = 6062U,
            NotCached = 6063U,
            UnauthorizedCaller = 6064U,
            MustBeCalledByStakingProgram = 6065U,
            ExpectedDelegateToMatchProvided = 6066U,
            CannotEffectTheSameStatTwice = 6067U,
            MintAuthorityRequiredForSFTs = 6068U
        }
    }

    namespace Types
    {
        public partial class CreateItemClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong? ParentOfParentClassIndex { get; set; }

            public ulong Space { get; set; }

            public ushort DesiredNamespaceArraySize { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public bool StoreMint { get; set; }

            public bool StoreMetadataFields { get; set; }

            public ItemClassData ItemClassData { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ParentOfParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentOfParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(Space, offset);
                offset += 8;
                _data.WriteU16(DesiredNamespaceArraySize, offset);
                offset += 2;
                if (UpdatePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UpdatePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteBool(StoreMint, offset);
                offset += 1;
                _data.WriteBool(StoreMetadataFields, offset);
                offset += 1;
                offset += ItemClassData.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateItemClassArgs result)
            {
                int offset = initialOffset;
                result = new CreateItemClassArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.ParentOfParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.Space = _data.GetU64(offset);
                offset += 8;
                result.DesiredNamespaceArraySize = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.StoreMint = _data.GetBool(offset);
                offset += 1;
                result.StoreMetadataFields = _data.GetBool(offset);
                offset += 1;
                offset += ItemClassData.Deserialize(_data, offset, out var resultItemClassData);
                result.ItemClassData = resultItemClassData;
                return offset - initialOffset;
            }
        }

        public partial class DrainItemClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UpdatePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UpdatePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainItemClassArgs result)
            {
                int offset = initialOffset;
                result = new DrainItemClassArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class UpdateItemClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public ItemClassData ItemClassData { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UpdatePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UpdatePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ItemClassData != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += ItemClassData.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateItemClassArgs result)
            {
                int offset = initialOffset;
                result = new UpdateItemClassArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                if (_data.GetBool(offset++))
                {
                    offset += ItemClassData.Deserialize(_data, offset, out var resultItemClassData);
                    result.ItemClassData = resultItemClassData;
                }

                return offset - initialOffset;
            }
        }

        public partial class DrainItemArgs
        {
            public ulong Index { get; set; }

            public ulong ClassIndex { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                if (UpdatePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UpdatePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainItemArgs result)
            {
                int offset = initialOffset;
                result = new DrainItemArgs();
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class CreateItemEscrowArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public ulong? NamespaceIndex { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PermissivenessType BuildPermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                if (NamespaceIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(NamespaceIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                if (BuildPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)BuildPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateItemEscrowArgs result)
            {
                int offset = initialOffset;
                result = new CreateItemEscrowArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.NamespaceIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.BuildPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class AddCraftItemToEscrowArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftItemIndex { get; set; }

            public ulong CraftItemClassIndex { get; set; }

            public PublicKey CraftItemClassMint { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public ulong AmountToContributeFromThisContributor { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey Originator { get; set; }

            public PermissivenessType BuildPermissivenessToUse { get; set; }

            public byte[][] ComponentProof { get; set; }

            public Component Component { get; set; }

            public CraftUsageInfo CraftUsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftItemIndex, offset);
                offset += 8;
                _data.WriteU64(CraftItemClassIndex, offset);
                offset += 8;
                _data.WritePubKey(CraftItemClassMint, offset);
                offset += 32;
                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WriteU64(AmountToContributeFromThisContributor, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemMint, offset);
                offset += 32;
                _data.WritePubKey(Originator, offset);
                offset += 32;
                if (BuildPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)BuildPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ComponentProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ComponentProof.Length, offset);
                    offset += 4;
                    foreach (var componentProofElement in ComponentProof)
                    {
                        _data.WriteSpan(componentProofElement, offset);
                        offset += componentProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Component != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Component.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (CraftUsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += CraftUsageInfo.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddCraftItemToEscrowArgs result)
            {
                int offset = initialOffset;
                result = new AddCraftItemToEscrowArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftItemIndex = _data.GetU64(offset);
                offset += 8;
                result.CraftItemClassIndex = _data.GetU64(offset);
                offset += 8;
                result.CraftItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.AmountToContributeFromThisContributor = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Originator = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.BuildPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                if (_data.GetBool(offset++))
                {
                    int resultComponentProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ComponentProof = new byte[resultComponentProofLength][];
                    for (uint resultComponentProofIdx = 0; resultComponentProofIdx < resultComponentProofLength; resultComponentProofIdx++)
                    {
                        result.ComponentProof[resultComponentProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += Component.Deserialize(_data, offset, out var resultComponent);
                    result.Component = resultComponent;
                }

                if (_data.GetBool(offset++))
                {
                    offset += CraftUsageInfo.Deserialize(_data, offset, out var resultCraftUsageInfo);
                    result.CraftUsageInfo = resultCraftUsageInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class StartItemEscrowBuildPhaseArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey Originator { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PermissivenessType BuildPermissivenessToUse { get; set; }

            public byte[][] EndNodeProof { get; set; }

            public ulong? TotalSteps { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(Originator, offset);
                offset += 32;
                _data.WritePubKey(NewItemMint, offset);
                offset += 32;
                if (BuildPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)BuildPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (EndNodeProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(EndNodeProof.Length, offset);
                    offset += 4;
                    foreach (var endNodeProofElement in EndNodeProof)
                    {
                        _data.WriteSpan(endNodeProofElement, offset);
                        offset += endNodeProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (TotalSteps != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(TotalSteps.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StartItemEscrowBuildPhaseArgs result)
            {
                int offset = initialOffset;
                result = new StartItemEscrowBuildPhaseArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.Originator = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.BuildPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                if (_data.GetBool(offset++))
                {
                    int resultEndNodeProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.EndNodeProof = new byte[resultEndNodeProofLength][];
                    for (uint resultEndNodeProofIdx = 0; resultEndNodeProofIdx < resultEndNodeProofLength; resultEndNodeProofIdx++)
                    {
                        result.EndNodeProof[resultEndNodeProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    result.TotalSteps = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }
        }

        public partial class CompleteItemEscrowBuildPhaseArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong NewItemIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public ulong Space { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey Originator { get; set; }

            public PermissivenessType BuildPermissivenessToUse { get; set; }

            public bool StoreMint { get; set; }

            public bool StoreMetadataFields { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(NewItemIndex, offset);
                offset += 8;
                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WriteU64(Space, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(Originator, offset);
                offset += 32;
                if (BuildPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)BuildPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteBool(StoreMint, offset);
                offset += 1;
                _data.WriteBool(StoreMetadataFields, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CompleteItemEscrowBuildPhaseArgs result)
            {
                int offset = initialOffset;
                result = new CompleteItemEscrowBuildPhaseArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.NewItemIndex = _data.GetU64(offset);
                offset += 8;
                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.Space = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.Originator = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.BuildPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.StoreMint = _data.GetBool(offset);
                offset += 1;
                result.StoreMetadataFields = _data.GetBool(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class UpdateItemArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateItemArgs result)
            {
                int offset = initialOffset;
                result = new UpdateItemArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class DeactivateItemEscrowArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey NewItemToken { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemToken, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DeactivateItemEscrowArgs result)
            {
                int offset = initialOffset;
                result = new DeactivateItemEscrowArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemToken = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class DrainItemEscrowArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey NewItemToken { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemToken, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainItemEscrowArgs result)
            {
                int offset = initialOffset;
                result = new DrainItemEscrowArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemToken = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class RemoveCraftItemFromEscrowArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong CraftItemIndex { get; set; }

            public ulong CraftEscrowIndex { get; set; }

            public string ComponentScope { get; set; }

            public ulong AmountToMake { get; set; }

            public ulong AmountContributedFromThisContributor { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey Originator { get; set; }

            public PublicKey CraftItemTokenMint { get; set; }

            public ulong CraftItemClassIndex { get; set; }

            public PublicKey CraftItemClassMint { get; set; }

            public PermissivenessType BuildPermissivenessToUse { get; set; }

            public byte[][] ComponentProof { get; set; }

            public Component Component { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                if (ParentClassIndex != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ParentClassIndex.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(CraftItemIndex, offset);
                offset += 8;
                _data.WriteU64(CraftEscrowIndex, offset);
                offset += 8;
                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU64(AmountToMake, offset);
                offset += 8;
                _data.WriteU64(AmountContributedFromThisContributor, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(NewItemMint, offset);
                offset += 32;
                _data.WritePubKey(Originator, offset);
                offset += 32;
                _data.WritePubKey(CraftItemTokenMint, offset);
                offset += 32;
                _data.WriteU64(CraftItemClassIndex, offset);
                offset += 8;
                _data.WritePubKey(CraftItemClassMint, offset);
                offset += 32;
                if (BuildPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)BuildPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ComponentProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ComponentProof.Length, offset);
                    offset += 4;
                    foreach (var componentProofElement in ComponentProof)
                    {
                        _data.WriteSpan(componentProofElement, offset);
                        offset += componentProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Component != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Component.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RemoveCraftItemFromEscrowArgs result)
            {
                int offset = initialOffset;
                result = new RemoveCraftItemFromEscrowArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.CraftItemIndex = _data.GetU64(offset);
                offset += 8;
                result.CraftEscrowIndex = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.AmountToMake = _data.GetU64(offset);
                offset += 8;
                result.AmountContributedFromThisContributor = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.NewItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Originator = _data.GetPubKey(offset);
                offset += 32;
                result.CraftItemTokenMint = _data.GetPubKey(offset);
                offset += 32;
                result.CraftItemClassIndex = _data.GetU64(offset);
                offset += 8;
                result.CraftItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.BuildPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                if (_data.GetBool(offset++))
                {
                    int resultComponentProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ComponentProof = new byte[resultComponentProofLength][];
                    for (uint resultComponentProofIdx = 0; resultComponentProofIdx < resultComponentProofLength; resultComponentProofIdx++)
                    {
                        result.ComponentProof[resultComponentProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += Component.Deserialize(_data, offset, out var resultComponent);
                    result.Component = resultComponent;
                }

                return offset - initialOffset;
            }
        }

        public partial class BeginItemActivationArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey ItemMint { get; set; }

            public byte ItemMarkerSpace { get; set; }

            public PermissivenessType UsagePermissivenessToUse { get; set; }

            public ulong Amount { get; set; }

            public ushort UsageIndex { get; set; }

            public PublicKey Target { get; set; }

            public UsageInfo UsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU8(ItemMarkerSpace, offset);
                offset += 1;
                if (UsagePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UsagePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                if (Target != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WritePubKey(Target, offset);
                    offset += 32;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageInfo.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BeginItemActivationArgs result)
            {
                int offset = initialOffset;
                result = new BeginItemActivationArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemMarkerSpace = _data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.UsagePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.Target = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    offset += UsageInfo.Deserialize(_data, offset, out var resultUsageInfo);
                    result.UsageInfo = resultUsageInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class ProveNewStateValidArgs
        {
            public byte[][][] UsageStateProofs { get; set; }

            public byte[][][] NewUsageStateProofs { get; set; }

            public ItemUsageState[] UsageStates { get; set; }

            public PublicKey ItemMint { get; set; }

            public ulong Index { get; set; }

            public ushort UsageIndex { get; set; }

            public ulong Amount { get; set; }

            public byte[][] UsageProof { get; set; }

            public ItemUsage Usage { get; set; }

            public ulong ClassIndex { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(UsageStateProofs.Length, offset);
                offset += 4;
                foreach (var usageStateProofsElement in UsageStateProofs)
                {
                    _data.WriteS32(usageStateProofsElement.Length, offset);
                    offset += 4;
                    foreach (var usageStateProofsElementElement in usageStateProofsElement)
                    {
                        _data.WriteSpan(usageStateProofsElementElement, offset);
                        offset += usageStateProofsElementElement.Length;
                    }
                }

                _data.WriteS32(NewUsageStateProofs.Length, offset);
                offset += 4;
                foreach (var newUsageStateProofsElement in NewUsageStateProofs)
                {
                    _data.WriteS32(newUsageStateProofsElement.Length, offset);
                    offset += 4;
                    foreach (var newUsageStateProofsElementElement in newUsageStateProofsElement)
                    {
                        _data.WriteSpan(newUsageStateProofsElementElement, offset);
                        offset += newUsageStateProofsElementElement.Length;
                    }
                }

                _data.WriteS32(UsageStates.Length, offset);
                offset += 4;
                foreach (var usageStatesElement in UsageStates)
                {
                    offset += usageStatesElement.Serialize(_data, offset);
                }

                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (UsageProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UsageProof.Length, offset);
                    offset += 4;
                    foreach (var usageProofElement in UsageProof)
                    {
                        _data.WriteSpan(usageProofElement, offset);
                        offset += usageProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Usage != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Usage.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ProveNewStateValidArgs result)
            {
                int offset = initialOffset;
                result = new ProveNewStateValidArgs();
                int resultUsageStateProofsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageStateProofs = new byte[resultUsageStateProofsLength][][];
                for (uint resultUsageStateProofsIdx = 0; resultUsageStateProofsIdx < resultUsageStateProofsLength; resultUsageStateProofsIdx++)
                {
                    int resultUsageStateProofsresultUsageStateProofsIdxLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UsageStateProofs[resultUsageStateProofsIdx] = new byte[resultUsageStateProofsresultUsageStateProofsIdxLength][];
                    for (uint resultUsageStateProofsresultUsageStateProofsIdxIdx = 0; resultUsageStateProofsresultUsageStateProofsIdxIdx < resultUsageStateProofsresultUsageStateProofsIdxLength; resultUsageStateProofsresultUsageStateProofsIdxIdx++)
                    {
                        result.UsageStateProofs[resultUsageStateProofsIdx][resultUsageStateProofsresultUsageStateProofsIdxIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                int resultNewUsageStateProofsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.NewUsageStateProofs = new byte[resultNewUsageStateProofsLength][][];
                for (uint resultNewUsageStateProofsIdx = 0; resultNewUsageStateProofsIdx < resultNewUsageStateProofsLength; resultNewUsageStateProofsIdx++)
                {
                    int resultNewUsageStateProofsresultNewUsageStateProofsIdxLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.NewUsageStateProofs[resultNewUsageStateProofsIdx] = new byte[resultNewUsageStateProofsresultNewUsageStateProofsIdxLength][];
                    for (uint resultNewUsageStateProofsresultNewUsageStateProofsIdxIdx = 0; resultNewUsageStateProofsresultNewUsageStateProofsIdxIdx < resultNewUsageStateProofsresultNewUsageStateProofsIdxLength; resultNewUsageStateProofsresultNewUsageStateProofsIdxIdx++)
                    {
                        result.NewUsageStateProofs[resultNewUsageStateProofsIdx][resultNewUsageStateProofsresultNewUsageStateProofsIdxIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                int resultUsageStatesLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageStates = new ItemUsageState[resultUsageStatesLength];
                for (uint resultUsageStatesIdx = 0; resultUsageStatesIdx < resultUsageStatesLength; resultUsageStatesIdx++)
                {
                    offset += ItemUsageState.Deserialize(_data, offset, out var resultUsageStatesresultUsageStatesIdx);
                    result.UsageStates[resultUsageStatesIdx] = resultUsageStatesresultUsageStatesIdx;
                }

                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    int resultUsageProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UsageProof = new byte[resultUsageProofLength][];
                    for (uint resultUsageProofIdx = 0; resultUsageProofIdx < resultUsageProofLength; resultUsageProofIdx++)
                    {
                        result.UsageProof[resultUsageProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += ItemUsage.Deserialize(_data, offset, out var resultUsage);
                    result.Usage = resultUsage;
                }

                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class ResetStateValidationForActivationArgs
        {
            public PublicKey ItemMint { get; set; }

            public ulong Index { get; set; }

            public ushort UsageIndex { get; set; }

            public ulong ClassIndex { get; set; }

            public ulong Amount { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public UsageInfo UsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                if (UsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageInfo.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ResetStateValidationForActivationArgs result)
            {
                int offset = initialOffset;
                result = new ResetStateValidationForActivationArgs();
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    offset += UsageInfo.Deserialize(_data, offset, out var resultUsageInfo);
                    result.UsageInfo = resultUsageInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class UpdateValidForUseIfWarmupPassedArgs
        {
            public PublicKey ItemMint { get; set; }

            public ulong Index { get; set; }

            public ushort UsageIndex { get; set; }

            public ulong ClassIndex { get; set; }

            public ulong Amount { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public byte[][] UsageProof { get; set; }

            public ItemUsage Usage { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                if (UsageProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UsageProof.Length, offset);
                    offset += 4;
                    foreach (var usageProofElement in UsageProof)
                    {
                        _data.WriteSpan(usageProofElement, offset);
                        offset += usageProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Usage != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Usage.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateValidForUseIfWarmupPassedArgs result)
            {
                int offset = initialOffset;
                result = new UpdateValidForUseIfWarmupPassedArgs();
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    int resultUsageProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UsageProof = new byte[resultUsageProofLength][];
                    for (uint resultUsageProofIdx = 0; resultUsageProofIdx < resultUsageProofLength; resultUsageProofIdx++)
                    {
                        result.UsageProof[resultUsageProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += ItemUsage.Deserialize(_data, offset, out var resultUsage);
                    result.Usage = resultUsage;
                }

                return offset - initialOffset;
            }
        }

        public partial class EndItemActivationArgs
        {
            public PublicKey ItemClassMint { get; set; }

            public PermissivenessType UsagePermissivenessToUse { get; set; }

            public ushort UsageIndex { get; set; }

            public ulong Index { get; set; }

            public ulong ClassIndex { get; set; }

            public ulong Amount { get; set; }

            public CraftUsageInfo UsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                if (UsagePermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UsagePermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU16(UsageIndex, offset);
                offset += 2;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (UsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageInfo.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EndItemActivationArgs result)
            {
                int offset = initialOffset;
                result = new EndItemActivationArgs();
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.UsagePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.UsageIndex = _data.GetU16(offset);
                offset += 2;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    offset += CraftUsageInfo.Deserialize(_data, offset, out var resultUsageInfo);
                    result.UsageInfo = resultUsageInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class UpdateTokensStakedArgs
        {
            public PublicKey ItemMint { get; set; }

            public ulong Index { get; set; }

            public bool Staked { get; set; }

            public ulong Amount { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteBool(Staked, offset);
                offset += 1;
                _data.WriteU64(Amount, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateTokensStakedArgs result)
            {
                int offset = initialOffset;
                result = new UpdateTokensStakedArgs();
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.Staked = _data.GetBool(offset);
                offset += 1;
                result.Amount = _data.GetU64(offset);
                offset += 8;
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

        public partial class ItemUsage
        {
            public ushort Index { get; set; }

            public BasicItemEffect[] BasicItemEffects { get; set; }

            public PermissivenessType[] UsagePermissiveness { get; set; }

            public InheritanceState Inherited { get; set; }

            public ItemClassType ItemClassType { get; set; }

            public Callback Callback { get; set; }

            public Callback Validation { get; set; }

            public bool DoNotPairWithSelf { get; set; }

            public DNPItem[] Dnp { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(Index, offset);
                offset += 2;
                if (BasicItemEffects != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(BasicItemEffects.Length, offset);
                    offset += 4;
                    foreach (var basicItemEffectsElement in BasicItemEffects)
                    {
                        offset += basicItemEffectsElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteS32(UsagePermissiveness.Length, offset);
                offset += 4;
                foreach (var usagePermissivenessElement in UsagePermissiveness)
                {
                    _data.WriteU8((byte)usagePermissivenessElement, offset);
                    offset += 1;
                }

                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                offset += ItemClassType.Serialize(_data, offset);
                if (Callback != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Callback.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

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

                _data.WriteBool(DoNotPairWithSelf, offset);
                offset += 1;
                if (Dnp != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(Dnp.Length, offset);
                    offset += 4;
                    foreach (var dnpElement in Dnp)
                    {
                        offset += dnpElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemUsage result)
            {
                int offset = initialOffset;
                result = new ItemUsage();
                result.Index = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    int resultBasicItemEffectsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.BasicItemEffects = new BasicItemEffect[resultBasicItemEffectsLength];
                    for (uint resultBasicItemEffectsIdx = 0; resultBasicItemEffectsIdx < resultBasicItemEffectsLength; resultBasicItemEffectsIdx++)
                    {
                        offset += BasicItemEffect.Deserialize(_data, offset, out var resultBasicItemEffectsresultBasicItemEffectsIdx);
                        result.BasicItemEffects[resultBasicItemEffectsIdx] = resultBasicItemEffectsresultBasicItemEffectsIdx;
                    }
                }

                int resultUsagePermissivenessLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsagePermissiveness = new PermissivenessType[resultUsagePermissivenessLength];
                for (uint resultUsagePermissivenessIdx = 0; resultUsagePermissivenessIdx < resultUsagePermissivenessLength; resultUsagePermissivenessIdx++)
                {
                    result.UsagePermissiveness[resultUsagePermissivenessIdx] = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                offset += ItemClassType.Deserialize(_data, offset, out var resultItemClassType);
                result.ItemClassType = resultItemClassType;
                if (_data.GetBool(offset++))
                {
                    offset += Callback.Deserialize(_data, offset, out var resultCallback);
                    result.Callback = resultCallback;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Callback.Deserialize(_data, offset, out var resultValidation);
                    result.Validation = resultValidation;
                }

                result.DoNotPairWithSelf = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    int resultDnpLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.Dnp = new DNPItem[resultDnpLength];
                    for (uint resultDnpIdx = 0; resultDnpIdx < resultDnpLength; resultDnpIdx++)
                    {
                        offset += DNPItem.Deserialize(_data, offset, out var resultDnpresultDnpIdx);
                        result.Dnp[resultDnpIdx] = resultDnpresultDnpIdx;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class ItemUsageState
        {
            public ushort Index { get; set; }

            public ulong Uses { get; set; }

            public ulong? ActivatedAt { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(Index, offset);
                offset += 2;
                _data.WriteU64(Uses, offset);
                offset += 8;
                if (ActivatedAt != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ActivatedAt.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemUsageState result)
            {
                int offset = initialOffset;
                result = new ItemUsageState();
                result.Index = _data.GetU16(offset);
                offset += 2;
                result.Uses = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ActivatedAt = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }
        }

        public partial class BasicItemEffect
        {
            public ulong Amount { get; set; }

            public string Stat { get; set; }

            public BasicItemEffectType ItemEffectType { get; set; }

            public ulong? ActiveDuration { get; set; }

            public ulong? StakingAmountNumerator { get; set; }

            public ulong? StakingAmountDivisor { get; set; }

            public ulong? StakingDurationNumerator { get; set; }

            public ulong? StakingDurationDivisor { get; set; }

            public ulong? MaxUses { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Amount, offset);
                offset += 8;
                offset += _data.WriteBorshString(Stat, offset);
                _data.WriteU8((byte)ItemEffectType, offset);
                offset += 1;
                if (ActiveDuration != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(ActiveDuration.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingAmountNumerator != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingAmountNumerator.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingAmountDivisor != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingAmountDivisor.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingDurationNumerator != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingDurationNumerator.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingDurationDivisor != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingDurationDivisor.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (MaxUses != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(MaxUses.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BasicItemEffect result)
            {
                int offset = initialOffset;
                result = new BasicItemEffect();
                result.Amount = _data.GetU64(offset);
                offset += 8;
                offset += _data.GetBorshString(offset, out var resultStat);
                result.Stat = resultStat;
                result.ItemEffectType = (BasicItemEffectType)_data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.ActiveDuration = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingAmountNumerator = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingAmountDivisor = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingDurationNumerator = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingDurationDivisor = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.MaxUses = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }
        }

        public partial class Component
        {
            public PublicKey Mint { get; set; }

            public ulong ClassIndex { get; set; }

            public ulong Amount { get; set; }

            public ulong? TimeToBuild { get; set; }

            public string ComponentScope { get; set; }

            public ushort UseUsageIndex { get; set; }

            public ComponentCondition Condition { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Mint, offset);
                offset += 32;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (TimeToBuild != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(TimeToBuild.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                offset += _data.WriteBorshString(ComponentScope, offset);
                _data.WriteU16(UseUsageIndex, offset);
                offset += 2;
                _data.WriteU8((byte)Condition, offset);
                offset += 1;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Component result)
            {
                int offset = initialOffset;
                result = new Component();
                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.TimeToBuild = _data.GetU64(offset);
                    offset += 8;
                }

                offset += _data.GetBorshString(offset, out var resultComponentScope);
                result.ComponentScope = resultComponentScope;
                result.UseUsageIndex = _data.GetU16(offset);
                offset += 2;
                result.Condition = (ComponentCondition)_data.GetU8(offset);
                offset += 1;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class Permissiveness
        {
            public InheritanceState Inherited { get; set; }

            public PermissivenessType PermissivenessType { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                _data.WriteU8((byte)PermissivenessType, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Permissiveness result)
            {
                int offset = initialOffset;
                result = new Permissiveness();
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                result.PermissivenessType = (PermissivenessType)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class ChildUpdatePropagationPermissiveness
        {
            public bool Overridable { get; set; }

            public InheritanceState Inherited { get; set; }

            public ChildUpdatePropagationPermissivenessType ChildUpdatePropagationPermissivenessType { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteBool(Overridable, offset);
                offset += 1;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                _data.WriteU8((byte)ChildUpdatePropagationPermissivenessType, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ChildUpdatePropagationPermissiveness result)
            {
                int offset = initialOffset;
                result = new ChildUpdatePropagationPermissiveness();
                result.Overridable = _data.GetBool(offset);
                offset += 1;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                result.ChildUpdatePropagationPermissivenessType = (ChildUpdatePropagationPermissivenessType)_data.GetU8(offset);
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

        public partial class DNPItem
        {
            public PublicKey Key { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Key, offset);
                offset += 32;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DNPItem result)
            {
                int offset = initialOffset;
                result = new DNPItem();
                result.Key = _data.GetPubKey(offset);
                offset += 32;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class Root
        {
            public InheritanceState Inherited { get; set; }

            public byte[] RootField { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                _data.WriteSpan(RootField, offset);
                offset += RootField.Length;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Root result)
            {
                int offset = initialOffset;
                result = new Root();
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                result.RootField = _data.GetBytes(offset, 32);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class Boolean
        {
            public InheritanceState Inherited { get; set; }

            public bool BooleanField { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                _data.WriteBool(BooleanField, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Boolean result)
            {
                int offset = initialOffset;
                result = new Boolean();
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                result.BooleanField = _data.GetBool(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class ItemClassSettings
        {
            public Boolean FreeBuild { get; set; }

            public Boolean ChildrenMustBeEditions { get; set; }

            public Boolean BuilderMustBeHolder { get; set; }

            public Permissiveness[] UpdatePermissiveness { get; set; }

            public Permissiveness[] BuildPermissiveness { get; set; }

            public ulong? StakingWarmUpDuration { get; set; }

            public ulong? StakingCooldownDuration { get; set; }

            public Permissiveness[] StakingPermissiveness { get; set; }

            public Permissiveness[] UnstakingPermissiveness { get; set; }

            public ChildUpdatePropagationPermissiveness[] ChildUpdatePropagationPermissiveness { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (FreeBuild != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += FreeBuild.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ChildrenMustBeEditions != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += ChildrenMustBeEditions.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (BuilderMustBeHolder != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += BuilderMustBeHolder.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UpdatePermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UpdatePermissiveness.Length, offset);
                    offset += 4;
                    foreach (var updatePermissivenessElement in UpdatePermissiveness)
                    {
                        offset += updatePermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (BuildPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(BuildPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var buildPermissivenessElement in BuildPermissiveness)
                    {
                        offset += buildPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingWarmUpDuration != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingWarmUpDuration.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingCooldownDuration != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingCooldownDuration.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(StakingPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var stakingPermissivenessElement in StakingPermissiveness)
                    {
                        offset += stakingPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UnstakingPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UnstakingPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var unstakingPermissivenessElement in UnstakingPermissiveness)
                    {
                        offset += unstakingPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ChildUpdatePropagationPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ChildUpdatePropagationPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var childUpdatePropagationPermissivenessElement in ChildUpdatePropagationPermissiveness)
                    {
                        offset += childUpdatePropagationPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemClassSettings result)
            {
                int offset = initialOffset;
                result = new ItemClassSettings();
                if (_data.GetBool(offset++))
                {
                    offset += Boolean.Deserialize(_data, offset, out var resultFreeBuild);
                    result.FreeBuild = resultFreeBuild;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Boolean.Deserialize(_data, offset, out var resultChildrenMustBeEditions);
                    result.ChildrenMustBeEditions = resultChildrenMustBeEditions;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Boolean.Deserialize(_data, offset, out var resultBuilderMustBeHolder);
                    result.BuilderMustBeHolder = resultBuilderMustBeHolder;
                }

                if (_data.GetBool(offset++))
                {
                    int resultUpdatePermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UpdatePermissiveness = new Permissiveness[resultUpdatePermissivenessLength];
                    for (uint resultUpdatePermissivenessIdx = 0; resultUpdatePermissivenessIdx < resultUpdatePermissivenessLength; resultUpdatePermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultUpdatePermissivenessresultUpdatePermissivenessIdx);
                        result.UpdatePermissiveness[resultUpdatePermissivenessIdx] = resultUpdatePermissivenessresultUpdatePermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultBuildPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.BuildPermissiveness = new Permissiveness[resultBuildPermissivenessLength];
                    for (uint resultBuildPermissivenessIdx = 0; resultBuildPermissivenessIdx < resultBuildPermissivenessLength; resultBuildPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultBuildPermissivenessresultBuildPermissivenessIdx);
                        result.BuildPermissiveness[resultBuildPermissivenessIdx] = resultBuildPermissivenessresultBuildPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingWarmUpDuration = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingCooldownDuration = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    int resultStakingPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.StakingPermissiveness = new Permissiveness[resultStakingPermissivenessLength];
                    for (uint resultStakingPermissivenessIdx = 0; resultStakingPermissivenessIdx < resultStakingPermissivenessLength; resultStakingPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultStakingPermissivenessresultStakingPermissivenessIdx);
                        result.StakingPermissiveness[resultStakingPermissivenessIdx] = resultStakingPermissivenessresultStakingPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultUnstakingPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UnstakingPermissiveness = new Permissiveness[resultUnstakingPermissivenessLength];
                    for (uint resultUnstakingPermissivenessIdx = 0; resultUnstakingPermissivenessIdx < resultUnstakingPermissivenessLength; resultUnstakingPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultUnstakingPermissivenessresultUnstakingPermissivenessIdx);
                        result.UnstakingPermissiveness[resultUnstakingPermissivenessIdx] = resultUnstakingPermissivenessresultUnstakingPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultChildUpdatePropagationPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ChildUpdatePropagationPermissiveness = new ChildUpdatePropagationPermissiveness[resultChildUpdatePropagationPermissivenessLength];
                    for (uint resultChildUpdatePropagationPermissivenessIdx = 0; resultChildUpdatePropagationPermissivenessIdx < resultChildUpdatePropagationPermissivenessLength; resultChildUpdatePropagationPermissivenessIdx++)
                    {
                        offset += ChildUpdatePropagationPermissiveness.Deserialize(_data, offset, out var resultChildUpdatePropagationPermissivenessresultChildUpdatePropagationPermissivenessIdx);
                        result.ChildUpdatePropagationPermissiveness[resultChildUpdatePropagationPermissivenessIdx] = resultChildUpdatePropagationPermissivenessresultChildUpdatePropagationPermissivenessIdx;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class ItemClassConfig
        {
            public Root UsageRoot { get; set; }

            public Root UsageStateRoot { get; set; }

            public Root ComponentRoot { get; set; }

            public ItemUsage[] Usages { get; set; }

            public Component[] Components { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (UsageRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UsageStateRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageStateRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ComponentRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += ComponentRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Usages != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(Usages.Length, offset);
                    offset += 4;
                    foreach (var usagesElement in Usages)
                    {
                        offset += usagesElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Components != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(Components.Length, offset);
                    offset += 4;
                    foreach (var componentsElement in Components)
                    {
                        offset += componentsElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemClassConfig result)
            {
                int offset = initialOffset;
                result = new ItemClassConfig();
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultUsageRoot);
                    result.UsageRoot = resultUsageRoot;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultUsageStateRoot);
                    result.UsageStateRoot = resultUsageStateRoot;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultComponentRoot);
                    result.ComponentRoot = resultComponentRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultUsagesLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.Usages = new ItemUsage[resultUsagesLength];
                    for (uint resultUsagesIdx = 0; resultUsagesIdx < resultUsagesLength; resultUsagesIdx++)
                    {
                        offset += ItemUsage.Deserialize(_data, offset, out var resultUsagesresultUsagesIdx);
                        result.Usages[resultUsagesIdx] = resultUsagesresultUsagesIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultComponentsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.Components = new Component[resultComponentsLength];
                    for (uint resultComponentsIdx = 0; resultComponentsIdx < resultComponentsLength; resultComponentsIdx++)
                    {
                        offset += Component.Deserialize(_data, offset, out var resultComponentsresultComponentsIdx);
                        result.Components[resultComponentsIdx] = resultComponentsresultComponentsIdx;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class ItemClassData
        {
            public ItemClassSettings Settings { get; set; }

            public ItemClassConfig Config { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += Settings.Serialize(_data, offset);
                offset += Config.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemClassData result)
            {
                int offset = initialOffset;
                result = new ItemClassData();
                offset += ItemClassSettings.Deserialize(_data, offset, out var resultSettings);
                result.Settings = resultSettings;
                offset += ItemClassConfig.Deserialize(_data, offset, out var resultConfig);
                result.Config = resultConfig;
                return offset - initialOffset;
            }
        }

        public partial class ItemActivationMarkerProofCounter
        {
            public ushort StatesProven { get; set; }

            public ushort StatesRequired { get; set; }

            public ushort IgnoreIndex { get; set; }

            public byte[] NewStateRoot { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(StatesProven, offset);
                offset += 2;
                _data.WriteU16(StatesRequired, offset);
                offset += 2;
                _data.WriteU16(IgnoreIndex, offset);
                offset += 2;
                _data.WriteSpan(NewStateRoot, offset);
                offset += NewStateRoot.Length;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemActivationMarkerProofCounter result)
            {
                int offset = initialOffset;
                result = new ItemActivationMarkerProofCounter();
                result.StatesProven = _data.GetU16(offset);
                offset += 2;
                result.StatesRequired = _data.GetU16(offset);
                offset += 2;
                result.IgnoreIndex = _data.GetU16(offset);
                offset += 2;
                result.NewStateRoot = _data.GetBytes(offset, 32);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class ItemData
        {
            public Root UsageStateRoot { get; set; }

            public ItemUsageState[] UsageStates { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (UsageStateRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += UsageStateRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UsageStates != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UsageStates.Length, offset);
                    offset += 4;
                    foreach (var usageStatesElement in UsageStates)
                    {
                        offset += usageStatesElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemData result)
            {
                int offset = initialOffset;
                result = new ItemData();
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultUsageStateRoot);
                    result.UsageStateRoot = resultUsageStateRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultUsageStatesLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UsageStates = new ItemUsageState[resultUsageStatesLength];
                    for (uint resultUsageStatesIdx = 0; resultUsageStatesIdx < resultUsageStatesLength; resultUsageStatesIdx++)
                    {
                        offset += ItemUsageState.Deserialize(_data, offset, out var resultUsageStatesresultUsageStatesIdx);
                        result.UsageStates[resultUsageStatesIdx] = resultUsageStatesresultUsageStatesIdx;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class CraftUsageInfo
        {
            public byte[][] CraftUsageStateProof { get; set; }

            public ItemUsageState CraftUsageState { get; set; }

            public byte[][] CraftUsageProof { get; set; }

            public ItemUsage CraftUsage { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(CraftUsageStateProof.Length, offset);
                offset += 4;
                foreach (var craftUsageStateProofElement in CraftUsageStateProof)
                {
                    _data.WriteSpan(craftUsageStateProofElement, offset);
                    offset += craftUsageStateProofElement.Length;
                }

                offset += CraftUsageState.Serialize(_data, offset);
                _data.WriteS32(CraftUsageProof.Length, offset);
                offset += 4;
                foreach (var craftUsageProofElement in CraftUsageProof)
                {
                    _data.WriteSpan(craftUsageProofElement, offset);
                    offset += craftUsageProofElement.Length;
                }

                offset += CraftUsage.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CraftUsageInfo result)
            {
                int offset = initialOffset;
                result = new CraftUsageInfo();
                int resultCraftUsageStateProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.CraftUsageStateProof = new byte[resultCraftUsageStateProofLength][];
                for (uint resultCraftUsageStateProofIdx = 0; resultCraftUsageStateProofIdx < resultCraftUsageStateProofLength; resultCraftUsageStateProofIdx++)
                {
                    result.CraftUsageStateProof[resultCraftUsageStateProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                offset += ItemUsageState.Deserialize(_data, offset, out var resultCraftUsageState);
                result.CraftUsageState = resultCraftUsageState;
                int resultCraftUsageProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.CraftUsageProof = new byte[resultCraftUsageProofLength][];
                for (uint resultCraftUsageProofIdx = 0; resultCraftUsageProofIdx < resultCraftUsageProofLength; resultCraftUsageProofIdx++)
                {
                    result.CraftUsageProof[resultCraftUsageProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                offset += ItemUsage.Deserialize(_data, offset, out var resultCraftUsage);
                result.CraftUsage = resultCraftUsage;
                return offset - initialOffset;
            }
        }

        public partial class UsageInfo
        {
            public byte[][] UsageProof { get; set; }

            public ItemUsage Usage { get; set; }

            public byte[][] UsageStateProof { get; set; }

            public ItemUsageState UsageState { get; set; }

            public byte[][] NewUsageStateProof { get; set; }

            public byte[] NewUsageStateRoot { get; set; }

            public ushort TotalStates { get; set; }

            public byte[][] TotalStatesProof { get; set; }

            public byte[][] NewTotalStatesProof { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(UsageProof.Length, offset);
                offset += 4;
                foreach (var usageProofElement in UsageProof)
                {
                    _data.WriteSpan(usageProofElement, offset);
                    offset += usageProofElement.Length;
                }

                offset += Usage.Serialize(_data, offset);
                _data.WriteS32(UsageStateProof.Length, offset);
                offset += 4;
                foreach (var usageStateProofElement in UsageStateProof)
                {
                    _data.WriteSpan(usageStateProofElement, offset);
                    offset += usageStateProofElement.Length;
                }

                offset += UsageState.Serialize(_data, offset);
                _data.WriteS32(NewUsageStateProof.Length, offset);
                offset += 4;
                foreach (var newUsageStateProofElement in NewUsageStateProof)
                {
                    _data.WriteSpan(newUsageStateProofElement, offset);
                    offset += newUsageStateProofElement.Length;
                }

                _data.WriteSpan(NewUsageStateRoot, offset);
                offset += NewUsageStateRoot.Length;
                _data.WriteU16(TotalStates, offset);
                offset += 2;
                _data.WriteS32(TotalStatesProof.Length, offset);
                offset += 4;
                foreach (var totalStatesProofElement in TotalStatesProof)
                {
                    _data.WriteSpan(totalStatesProofElement, offset);
                    offset += totalStatesProofElement.Length;
                }

                _data.WriteS32(NewTotalStatesProof.Length, offset);
                offset += 4;
                foreach (var newTotalStatesProofElement in NewTotalStatesProof)
                {
                    _data.WriteSpan(newTotalStatesProofElement, offset);
                    offset += newTotalStatesProofElement.Length;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UsageInfo result)
            {
                int offset = initialOffset;
                result = new UsageInfo();
                int resultUsageProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageProof = new byte[resultUsageProofLength][];
                for (uint resultUsageProofIdx = 0; resultUsageProofIdx < resultUsageProofLength; resultUsageProofIdx++)
                {
                    result.UsageProof[resultUsageProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                offset += ItemUsage.Deserialize(_data, offset, out var resultUsage);
                result.Usage = resultUsage;
                int resultUsageStateProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageStateProof = new byte[resultUsageStateProofLength][];
                for (uint resultUsageStateProofIdx = 0; resultUsageStateProofIdx < resultUsageStateProofLength; resultUsageStateProofIdx++)
                {
                    result.UsageStateProof[resultUsageStateProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                offset += ItemUsageState.Deserialize(_data, offset, out var resultUsageState);
                result.UsageState = resultUsageState;
                int resultNewUsageStateProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.NewUsageStateProof = new byte[resultNewUsageStateProofLength][];
                for (uint resultNewUsageStateProofIdx = 0; resultNewUsageStateProofIdx < resultNewUsageStateProofLength; resultNewUsageStateProofIdx++)
                {
                    result.NewUsageStateProof[resultNewUsageStateProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                result.NewUsageStateRoot = _data.GetBytes(offset, 32);
                offset += 32;
                result.TotalStates = _data.GetU16(offset);
                offset += 2;
                int resultTotalStatesProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.TotalStatesProof = new byte[resultTotalStatesProofLength][];
                for (uint resultTotalStatesProofIdx = 0; resultTotalStatesProofIdx < resultTotalStatesProofLength; resultTotalStatesProofIdx++)
                {
                    result.TotalStatesProof[resultTotalStatesProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                int resultNewTotalStatesProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.NewTotalStatesProof = new byte[resultNewTotalStatesProofLength][];
                for (uint resultNewTotalStatesProofIdx = 0; resultNewTotalStatesProofIdx < resultNewTotalStatesProofLength; resultNewTotalStatesProofIdx++)
                {
                    result.NewTotalStatesProof[resultNewTotalStatesProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                return offset - initialOffset;
            }
        }

        public enum ItemClassTypeType : byte
        {
            Wearable,
            Consumable
        }

        public partial class WearableType
        {
            public string[] BodyPart { get; set; }

            public ulong? LimitPerPart { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out WearableType result)
            {
                int offset = initialOffset;
                result = new WearableType();
                int resultBodyPartLength = (int)_data.GetU32(offset);
                offset += 4;
                result.BodyPart = new string[resultBodyPartLength];
                for (uint resultBodyPartIdx = 0; resultBodyPartIdx < resultBodyPartLength; resultBodyPartIdx++)
                {
                    offset += _data.GetBorshString(offset, out var resultBodyPartresultBodyPartIdx);
                    result.BodyPart[resultBodyPartIdx] = resultBodyPartresultBodyPartIdx;
                }

                if (_data.GetBool(offset++))
                {
                    result.LimitPerPart = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(BodyPart.Length, offset);
                offset += 4;
                foreach (var bodyPartElement in BodyPart)
                {
                    offset += _data.WriteBorshString(bodyPartElement, offset);
                }

                if (LimitPerPart != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(LimitPerPart.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class ConsumableType
        {
            public ulong? MaxUses { get; set; }

            public ulong? MaxPlayersPerUse { get; set; }

            public ItemUsageType ItemUsageType { get; set; }

            public ulong? CooldownDuration { get; set; }

            public ulong? WarmupDuration { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ConsumableType result)
            {
                int offset = initialOffset;
                result = new ConsumableType();
                if (_data.GetBool(offset++))
                {
                    result.MaxUses = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.MaxPlayersPerUse = _data.GetU64(offset);
                    offset += 8;
                }

                result.ItemUsageType = (ItemUsageType)_data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.CooldownDuration = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.WarmupDuration = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (MaxUses != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(MaxUses.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (MaxPlayersPerUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(MaxPlayersPerUse.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU8((byte)ItemUsageType, offset);
                offset += 1;
                if (CooldownDuration != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(CooldownDuration.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (WarmupDuration != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(WarmupDuration.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class ItemClassType
        {
            public WearableType WearableValue { get; set; }

            public ConsumableType ConsumableValue { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Type, offset);
                offset += 1;
                switch (Type)
                {
                    case ItemClassTypeType.Wearable:
                        offset += WearableValue.Serialize(_data, offset);
                        break;
                    case ItemClassTypeType.Consumable:
                        offset += ConsumableValue.Serialize(_data, offset);
                        break;
                }

                return offset - initialOffset;
            }

            public ItemClassTypeType Type { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ItemClassType result)
            {
                int offset = initialOffset;
                result = new ItemClassType();
                result.Type = (ItemClassTypeType)_data.GetU8(offset);
                offset += 1;
                switch (result.Type)
                {
                    case ItemClassTypeType.Wearable:
                    {
                        WearableType tmpWearableValue = new WearableType();
                        offset += WearableType.Deserialize(_data, offset, out tmpWearableValue);
                        result.WearableValue = tmpWearableValue;
                        break;
                    }

                    case ItemClassTypeType.Consumable:
                    {
                        ConsumableType tmpConsumableValue = new ConsumableType();
                        offset += ConsumableType.Deserialize(_data, offset, out tmpConsumableValue);
                        result.ConsumableValue = tmpConsumableValue;
                        break;
                    }
                }

                return offset - initialOffset;
            }
        }

        public enum ItemUsageType : byte
        {
            Exhaustion,
            Destruction,
            Infinite
        }

        public enum BasicItemEffectType : byte
        {
            Increment,
            Decrement,
            IncrementPercent,
            DecrementPercent,
            IncrementPercentFromBase,
            DecrementPercentFromBase
        }

        public enum ComponentCondition : byte
        {
            Consumed,
            Presence,
            Absence,
            Cooldown,
            CooldownAndConsume
        }

        public enum PermissivenessType : byte
        {
            TokenHolder,
            ParentTokenHolder,
            UpdateAuthority,
            Anybody
        }

        public enum ChildUpdatePropagationPermissivenessType : byte
        {
            Usages,
            Components,
            UpdatePermissiveness,
            BuildPermissiveness,
            ChildUpdatePropagationPermissiveness,
            ChildrenMustBeEditionsPermissiveness,
            BuilderMustBeHolderPermissiveness,
            StakingPermissiveness,
            Namespaces,
            FreeBuildPermissiveness
        }

        public enum InheritanceState : byte
        {
            NotInherited,
            Inherited,
            Overridden
        }
    }

    public partial class RaindropsItemClient : TransactionalBaseClient<RaindropsItemErrorKind>
    {
        public RaindropsItemClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemClass>>> GetItemClasssAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = ItemClass.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemClass>>(res);
            List<ItemClass> resultingAccounts = new List<ItemClass>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => ItemClass.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemClass>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemEscrow>>> GetItemEscrowsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = ItemEscrow.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemEscrow>>(res);
            List<ItemEscrow> resultingAccounts = new List<ItemEscrow>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => ItemEscrow.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemEscrow>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<CraftItemCounter>>> GetCraftItemCountersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = CraftItemCounter.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<CraftItemCounter>>(res);
            List<CraftItemCounter> resultingAccounts = new List<CraftItemCounter>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => CraftItemCounter.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<CraftItemCounter>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemActivationMarker>>> GetItemActivationMarkersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = ItemActivationMarker.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemActivationMarker>>(res);
            List<ItemActivationMarker> resultingAccounts = new List<ItemActivationMarker>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => ItemActivationMarker.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<ItemActivationMarker>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Item>>> GetItemsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = Item.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Item>>(res);
            List<Item> resultingAccounts = new List<Item>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Item.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Item>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<ItemClass>> GetItemClassAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemClass>(res);
            var resultingAccount = ItemClass.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemClass>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<ItemEscrow>> GetItemEscrowAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemEscrow>(res);
            var resultingAccount = ItemEscrow.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemEscrow>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<CraftItemCounter>> GetCraftItemCounterAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<CraftItemCounter>(res);
            var resultingAccount = CraftItemCounter.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<CraftItemCounter>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<ItemActivationMarker>> GetItemActivationMarkerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemActivationMarker>(res);
            var resultingAccount = ItemActivationMarker.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<ItemActivationMarker>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<Item>> GetItemAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<Item>(res);
            var resultingAccount = Item.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<Item>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeItemClassAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, ItemClass> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                ItemClass parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = ItemClass.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeItemEscrowAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, ItemEscrow> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                ItemEscrow parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = ItemEscrow.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeCraftItemCounterAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, CraftItemCounter> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                CraftItemCounter parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = CraftItemCounter.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeItemActivationMarkerAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, ItemActivationMarker> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                ItemActivationMarker parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = ItemActivationMarker.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeItemAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, Item> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Item parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Item.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendCreateItemClassAsync(CreateItemClassAccounts accounts, CreateItemClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.CreateItemClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateItemClassAsync(UpdateItemClassAccounts accounts, UpdateItemClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.UpdateItemClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainItemClassAsync(DrainItemClassAccounts accounts, DrainItemClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.DrainItemClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainItemAsync(DrainItemAccounts accounts, DrainItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.DrainItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCreateItemEscrowAsync(CreateItemEscrowAccounts accounts, CreateItemEscrowArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.CreateItemEscrow(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendAddCraftItemToEscrowAsync(AddCraftItemToEscrowAccounts accounts, AddCraftItemToEscrowArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.AddCraftItemToEscrow(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRemoveCraftItemFromEscrowAsync(RemoveCraftItemFromEscrowAccounts accounts, RemoveCraftItemFromEscrowArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.RemoveCraftItemFromEscrow(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendStartItemEscrowBuildPhaseAsync(StartItemEscrowBuildPhaseAccounts accounts, StartItemEscrowBuildPhaseArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.StartItemEscrowBuildPhase(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCompleteItemEscrowBuildPhaseAsync(CompleteItemEscrowBuildPhaseAccounts accounts, CompleteItemEscrowBuildPhaseArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.CompleteItemEscrowBuildPhase(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateItemAsync(UpdateItemAccounts accounts, UpdateItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.UpdateItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDeactivateItemEscrowAsync(DeactivateItemEscrowAccounts accounts, DeactivateItemEscrowArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.DeactivateItemEscrow(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainItemEscrowAsync(DrainItemEscrowAccounts accounts, DrainItemEscrowArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.DrainItemEscrow(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBeginItemActivationAsync(BeginItemActivationAccounts accounts, BeginItemActivationArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.BeginItemActivation(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendEndItemActivationAsync(EndItemActivationAccounts accounts, EndItemActivationArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.EndItemActivation(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendResetStateValidationForActivationAsync(ResetStateValidationForActivationAccounts accounts, ResetStateValidationForActivationArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ResetStateValidationForActivation(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateValidForUseIfWarmupPassedAsync(UpdateValidForUseIfWarmupPassedAccounts accounts, UpdateValidForUseIfWarmupPassedArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.UpdateValidForUseIfWarmupPassed(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendProveNewStateValidAsync(ProveNewStateValidAccounts accounts, ProveNewStateValidArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ProveNewStateValid(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendItemArtifactJoinNamespaceAsync(ItemArtifactJoinNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ItemArtifactJoinNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendItemArtifactLeaveNamespaceAsync(ItemArtifactLeaveNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ItemArtifactLeaveNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendItemArtifactCacheNamespaceAsync(ItemArtifactCacheNamespaceAccounts accounts, ulong page, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ItemArtifactCacheNamespace(accounts, page, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendItemArtifactUncacheNamespaceAsync(ItemArtifactUncacheNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.ItemArtifactUncacheNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateTokensStakedAsync(UpdateTokensStakedAccounts accounts, UpdateTokensStakedArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsItemProgram.UpdateTokensStaked(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<RaindropsItemErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<RaindropsItemErrorKind>>{{6000U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.IncorrectOwner, "Account does not have correct owner!")}, {6001U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.Uninitialized, "Account is not initialized!")}, {6002U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MintMismatch, "Mint Mismatch!")}, {6003U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.TokenTransferFailed, "Token transfer failed")}, {6004U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NumericalOverflowError, "Numerical overflow error")}, {6005U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.TokenMintToFailed, "Token mint to failed")}, {6006U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.TokenBurnFailed, "TokenBurnFailed")}, {6007U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.DerivedKeyInvalid, "Derived key is invalid")}, {6008U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustSpecifyPermissivenessType, "Must specify permissiveness type")}, {6009U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.PermissivenessNotFound, "Permissiveness not found in array")}, {6010U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.PublicKeyMismatch, "Public key mismatch")}, {6011U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.InsufficientBalance, "Insufficient Balance")}, {6012U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MetadataDoesntExist, "Metadata doesn't exist")}, {6013U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.EditionDoesntExist, "Edition doesn't exist")}, {6014U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NoParentPresent, "No parent present")}, {6015U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ExpectedParent, "Expected parent")}, {6016U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.InvalidMintAuthority, "Invalid mint authority")}, {6017U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NotMintAuthority, "Not mint authority")}, {6018U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CannotMakeZero, "Cannot make zero of an item")}, {6019U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustBeHolderToBuild, "Must be token holder to build against it")}, {6020U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.InvalidConfigForFungibleMints, "This config is invalid for fungible mints")}, {6021U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MissingMerkleInfo, "Missing the merkle fields")}, {6022U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.InvalidProof, "Invalid proof")}, {6023U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ItemReadyForCompletion, "Item ready for completion")}, {6024U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustUseMerkleOrComponentList, "In order for crafting to work there must be either a component list or a component merkle root")}, {6025U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustUseMerkleOrUsageState, "In order for crafting to work there must be either a usage state list on the craft component or a usage merkle root")}, {6026U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.UnableToFindValidCooldownState, "Unable to find a valid cooldown state")}, {6027U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.BalanceNeedsToBeZero, "Balance needs to be zero")}, {6028U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NotPartOfComponentScope, "This component is not part of this escrow's component scope")}, {6029U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.TimeToBuildMismatch, "The time to build on two disparate components in the same scope is different. Either unset one or make them both the same.")}, {6030U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.StakingMintNotWhitelisted, "This staking mint has not been whitelisted in this namespace")}, {6031U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.BuildPhaseNotStarted, "Build phase not started")}, {6032U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.BuildPhaseNotFinished, "Build phase not finished")}, {6033U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.DeactivatedItemEscrow, "Item escrow has been deactivated")}, {6034U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.BuildPhaseAlreadyStarted, "Build phase already started")}, {6035U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.StillMissingComponents, "You havent added all components to the escrow")}, {6036U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ChildrenStillExist, "You cannot delete this class until all children are deleted")}, {6037U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.UnstakeTokensFirst, "An item cannot be destroyed until all its staked tokens are unstaked")}, {6038U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.AlreadyDeactivated, "Already deactivated")}, {6039U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NotDeactivated, "Escrow not deactivated")}, {6040U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NotEmptied, "Item escrow not emptied")}, {6041U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.GivingTooMuch, "You do not need to provide this many of this component to make your recipe")}, {6042U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustProvideUsageIndex, "Must provide usage index")}, {6043U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CannotUseItemWithoutUsageOrMerkle, "An item and item class must either use usage roots or merkles, if neither are present, item is unusable")}, {6044U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MaxUsesReached, "Max uses reached")}, {6045U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CooldownNotOver, "Cooldown not finished")}, {6046U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CannotUseWearable, "Cannot use wearable")}, {6047U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.UsageIndexMismatch, "Usage index mismatch")}, {6048U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ProvingNewStateNotRequired, "Proving new state not required")}, {6049U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustSubmitStatesInOrder, "You must submit proofs in order to revalidate the new state.")}, {6050U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ItemActivationNotValidYet, "Item activation marker not valid yet")}, {6051U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.WarmupNotFinished, "Warmup not finished")}, {6052U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustBeChild, "Must be a child edition")}, {6053U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustUseRealScope, "Must use real scope to build")}, {6054U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CraftClassIndexMismatch, "The class index passed up does not match that on the component")}, {6055U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustBeGreaterThanZero, "Must use at least one of this item")}, {6056U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.AtaShouldNotHaveDelegate, "To use an ata in this contract, please remove its delegate first")}, {6057U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ReinitializationDetected, "Reinitialization hack detected")}, {6058U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.FailedToJoinNamespace, "Failed to join namespace")}, {6059U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.FailedToLeaveNamespace, "Failed to leave namespace")}, {6060U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.FailedToCache, "Failed to cache")}, {6061U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.FailedToUncache, "Failed to uncache")}, {6062U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.AlreadyCached, "Already cached")}, {6063U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.NotCached, "Not cached")}, {6064U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.UnauthorizedCaller, "Unauthorized Caller")}, {6065U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MustBeCalledByStakingProgram, "Must be called by staking program")}, {6066U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.ExpectedDelegateToMatchProvided, "Expected delegate to match provided")}, {6067U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.CannotEffectTheSameStatTwice, "Cannot affect the same stat twice")}, {6068U, new ProgramError<RaindropsItemErrorKind>(RaindropsItemErrorKind.MintAuthorityRequiredForSFTs, "Cannot mint an SFT without mint auth")}, };
        }
    }

    namespace Program
    {
        public class CreateItemClassAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public PublicKey Parent { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class UpdateItemClassAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey Parent { get; set; }
        }

        public class DrainItemClassAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ParentClass { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class DrainItemAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class CreateItemEscrowAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemClassMetadata { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey NewItemMetadata { get; set; }

            public PublicKey NewItemEdition { get; set; }

            public PublicKey ItemEscrow { get; set; }

            public PublicKey NewItemToken { get; set; }

            public PublicKey NewItemTokenHolder { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class AddCraftItemToEscrowAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemEscrow { get; set; }

            public PublicKey CraftItemCounter { get; set; }

            public PublicKey NewItemToken { get; set; }

            public PublicKey NewItemTokenHolder { get; set; }

            public PublicKey CraftItemTokenAccountEscrow { get; set; }

            public PublicKey CraftItemTokenMint { get; set; }

            public PublicKey CraftItemTokenAccount { get; set; }

            public PublicKey CraftItem { get; set; }

            public PublicKey CraftItemClass { get; set; }

            public PublicKey CraftItemTransferAuthority { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class RemoveCraftItemFromEscrowAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemEscrow { get; set; }

            public PublicKey CraftItemCounter { get; set; }

            public PublicKey NewItemToken { get; set; }

            public PublicKey NewItemTokenHolder { get; set; }

            public PublicKey CraftItemTokenAccountEscrow { get; set; }

            public PublicKey CraftItemTokenAccount { get; set; }

            public PublicKey CraftItem { get; set; }

            public PublicKey CraftItemClass { get; set; }

            public PublicKey Receiver { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }
        }

        public class StartItemEscrowBuildPhaseAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey ItemEscrow { get; set; }

            public PublicKey NewItemToken { get; set; }

            public PublicKey NewItemTokenHolder { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class CompleteItemEscrowBuildPhaseAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey NewItem { get; set; }

            public PublicKey NewItemMint { get; set; }

            public PublicKey NewItemMetadata { get; set; }

            public PublicKey NewItemEdition { get; set; }

            public PublicKey ItemEscrow { get; set; }

            public PublicKey NewItemToken { get; set; }

            public PublicKey NewItemTokenHolder { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class UpdateItemAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey Item { get; set; }
        }

        public class DeactivateItemEscrowAccounts
        {
            public PublicKey ItemEscrow { get; set; }

            public PublicKey Originator { get; set; }
        }

        public class DrainItemEscrowAccounts
        {
            public PublicKey ItemEscrow { get; set; }

            public PublicKey Originator { get; set; }
        }

        public class BeginItemActivationAccounts
        {
            public PublicKey ItemClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Clock { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey ValidationProgram { get; set; }
        }

        public class EndItemActivationAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemTransferAuthority { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class ResetStateValidationForActivationAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemActivationMarker { get; set; }
        }

        public class UpdateValidForUseIfWarmupPassedAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class ProveNewStateValidAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class ItemArtifactJoinNamespaceAccounts
        {
            public PublicKey ItemArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class ItemArtifactLeaveNamespaceAccounts
        {
            public PublicKey ItemArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class ItemArtifactCacheNamespaceAccounts
        {
            public PublicKey ItemArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class ItemArtifactUncacheNamespaceAccounts
        {
            public PublicKey ItemArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class UpdateTokensStakedAccounts
        {
            public PublicKey Item { get; set; }

            public PublicKey InstructionSysvarAccount { get; set; }
        }

        public static class RaindropsItemProgram
        {
            public static Solana.Unity.Rpc.Models.TransactionInstruction CreateItemClass(CreateItemClassAccounts accounts, CreateItemClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Edition, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Parent, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(10386161991404581802UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateItemClass(UpdateItemClassAccounts accounts, UpdateItemClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Parent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13168653294155805353UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainItemClass(DrainItemClassAccounts accounts, DrainItemClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ParentClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(4106148667849034199UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainItem(DrainItemAccounts accounts, DrainItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(3305797630540197013UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction CreateItemEscrow(CreateItemEscrowAccounts accounts, CreateItemEscrowArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClassMetadata, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NewItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemMetadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemEdition, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(9032186023711866296UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction AddCraftItemToEscrow(AddCraftItemToEscrowAccounts accounts, AddCraftItemToEscrowArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemCounter, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemTokenAccountEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemTokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemTokenAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItem, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.CraftItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.CraftItemTransferAuthority, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11458874557531436766UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction RemoveCraftItemFromEscrow(RemoveCraftItemFromEscrowAccounts accounts, RemoveCraftItemFromEscrowArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemCounter, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemTokenAccountEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItemTokenAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.CraftItem, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.CraftItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(4891289200433464017UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction StartItemEscrowBuildPhase(StartItemEscrowBuildPhaseAccounts accounts, StartItemEscrowBuildPhaseArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(8239743949782457317UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction CompleteItemEscrowBuildPhase(CompleteItemEscrowBuildPhaseAccounts accounts, CompleteItemEscrowBuildPhaseArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NewItem, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NewItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemMetadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemEdition, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NewItemToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewItemTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(2290806659230676273UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateItem(UpdateItemAccounts accounts, UpdateItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13306981143299284508UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DeactivateItemEscrow(DeactivateItemEscrowAccounts accounts, DeactivateItemEscrowArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Originator, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6696384930677595901UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainItemEscrow(DrainItemEscrowAccounts accounts, DrainItemEscrowArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Originator, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14804687758159204417UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction BeginItemActivation(BeginItemActivationAccounts accounts, BeginItemActivationArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14099441617090695165UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction EndItemActivation(EndItemActivationAccounts accounts, EndItemActivationArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemTransferAuthority, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Receiver, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(5567636754757841514UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ResetStateValidationForActivation(ResetStateValidationForActivationAccounts accounts, ResetStateValidationForActivationArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(10128033177487644106UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateValidForUseIfWarmupPassed(UpdateValidForUseIfWarmupPassedAccounts accounts, UpdateValidForUseIfWarmupPassedArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(15848927421726065180UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ProveNewStateValid(ProveNewStateValidAccounts accounts, ProveNewStateValidArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16775412242876183744UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ItemArtifactJoinNamespace(ItemArtifactJoinNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13404351263069356450UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ItemArtifactLeaveNamespace(ItemArtifactLeaveNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11531560402242701568UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ItemArtifactCacheNamespace(ItemArtifactCacheNamespaceAccounts accounts, ulong page, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1881338283712329277UL, offset);
                offset += 8;
                _data.WriteU64(page, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ItemArtifactUncacheNamespace(ItemArtifactUncacheNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(3433735974984696887UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateTokensStaked(UpdateTokensStakedAccounts accounts, UpdateTokensStakedArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.InstructionSysvarAccount, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(17967048106787078910UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }
}