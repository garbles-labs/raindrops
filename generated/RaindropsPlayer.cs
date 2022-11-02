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
using RaindropsPlayer;
using RaindropsPlayer.Program;
using RaindropsPlayer.Errors;
using RaindropsPlayer.Accounts;
using RaindropsPlayer.Types;

namespace RaindropsPlayer
{
    namespace Accounts
    {
        public partial class PlayerClass
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 6233302514812794058UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{202, 52, 247, 156, 156, 35, 129, 86};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "apfJ6v58TLh";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public PublicKey Parent { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public PlayerClassData Data { get; set; }

            public ulong ExistingChildren { get; set; }

            public byte Bump { get; set; }

            public static PlayerClass Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                PlayerClass result = new PlayerClass();
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
                    result.Metadata = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.Edition = _data.GetPubKey(offset);
                    offset += 32;
                }

                offset += PlayerClassData.Deserialize(_data, offset, out var resultData);
                result.Data = resultData;
                result.ExistingChildren = _data.GetU64(offset);
                offset += 8;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                return result;
            }
        }

        public partial class Player
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 15766710478567431885UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{205, 222, 112, 7, 165, 155, 206, 218};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "bSBoKNsSHuj";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public byte Padding { get; set; }

            public PublicKey Parent { get; set; }

            public ulong ClassIndex { get; set; }

            public PublicKey Mint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public byte Bump { get; set; }

            public ulong TokensStaked { get; set; }

            public ulong ActiveItemCounter { get; set; }

            public ulong ItemsInBackpack { get; set; }

            public PlayerData Data { get; set; }

            public EquippedItem[] EquippedItems { get; set; }

            public ulong TokensPaidIn { get; set; }

            public static Player Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                Player result = new Player();
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
                result.ActiveItemCounter = _data.GetU64(offset);
                offset += 8;
                result.ItemsInBackpack = _data.GetU64(offset);
                offset += 8;
                offset += PlayerData.Deserialize(_data, offset, out var resultData);
                result.Data = resultData;
                int resultEquippedItemsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.EquippedItems = new EquippedItem[resultEquippedItemsLength];
                for (uint resultEquippedItemsIdx = 0; resultEquippedItemsIdx < resultEquippedItemsLength; resultEquippedItemsIdx++)
                {
                    offset += EquippedItem.Deserialize(_data, offset, out var resultEquippedItemsresultEquippedItemsIdx);
                    result.EquippedItems[resultEquippedItemsIdx] = resultEquippedItemsresultEquippedItemsIdx;
                }

                result.TokensPaidIn = _data.GetU64(offset);
                offset += 8;
                return result;
            }
        }

        public partial class PlayerItemActivationMarker
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 9333922393358517221UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{229, 195, 50, 169, 246, 192, 136, 129};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "fRz57rwaSs2";
            public byte Bump { get; set; }

            public PublicKey Player { get; set; }

            public PublicKey Item { get; set; }

            public ushort UsageIndex { get; set; }

            public BasicItemEffect[] BasicItemEffects { get; set; }

            public byte[] RemovedBieBitmap { get; set; }

            public ulong Amount { get; set; }

            public ulong ActivatedAt { get; set; }

            public ulong ActiveItemCounter { get; set; }

            public static PlayerItemActivationMarker Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                PlayerItemActivationMarker result = new PlayerItemActivationMarker();
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.Player = _data.GetPubKey(offset);
                offset += 32;
                result.Item = _data.GetPubKey(offset);
                offset += 32;
                result.UsageIndex = _data.GetU16(offset);
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

                if (_data.GetBool(offset++))
                {
                    int resultRemovedBieBitmapLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.RemovedBieBitmap = _data.GetBytes(offset, resultRemovedBieBitmapLength);
                    offset += resultRemovedBieBitmapLength;
                }

                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.ActivatedAt = _data.GetU64(offset);
                offset += 8;
                result.ActiveItemCounter = _data.GetU64(offset);
                offset += 8;
                return result;
            }
        }
    }

    namespace Errors
    {
        public enum RaindropsPlayerErrorKind : uint
        {
            IncorrectOwner = 6000U,
            Uninitialized = 6001U,
            MintMismatch = 6002U,
            TokenTransferFailed = 6003U,
            NumericalOverflowError = 6004U,
            TokenMintToFailed = 6005U,
            TokenBurnFailed = 6006U,
            DerivedKeyInvalid = 6007U,
            NoParentPresent = 6008U,
            ExpectedParent = 6009U,
            ChildrenStillExist = 6010U,
            UnstakeTokensFirst = 6011U,
            MustBeHolderToBuild = 6012U,
            CannotRemoveThisMuch = 6013U,
            UsageRootNotPresent = 6014U,
            InvalidProof = 6015U,
            ItemContainsNoUsages = 6016U,
            FoundNoMatchingUsage = 6017U,
            CannotEquipConsumable = 6018U,
            BodyPartNotEligible = 6019U,
            CannotUnequipThisMuch = 6020U,
            BodyPartContainsTooManyOfThisType = 6021U,
            BodyPartContainsTooMany = 6022U,
            CannotEquipItemWithoutUsageOrMerkle = 6023U,
            NoBodyPartsToEquip = 6024U,
            UnableToFindBodyPartByIndex = 6025U,
            ItemCannotBePairedWithSelf = 6026U,
            ItemCannotBeEquippedWithDNPEntry = 6027U,
            BasicStatTemplateTypeDoesNotMatchBasicStatType = 6028U,
            CannotAlterThisTypeNumerically = 6029U,
            Unreachable = 6030U,
            NotValidForUseYet = 6031U,
            RemoveEquipmentFirst = 6032U,
            DeactivateAllItemsFirst = 6033U,
            RemoveAllItemsFromBackpackFirst = 6034U,
            InsufficientBalance = 6035U,
            PermissivenessNotFound = 6036U,
            MustSpecifyPermissivenessType = 6037U,
            IndexAlreadyUsed = 6038U,
            NameAlreadyUsed = 6039U,
            CannotResetPlayerStatsUntilItemEffectsAreRemoved = 6040U,
            FailedToJoinNamespace = 6041U,
            FailedToLeaveNamespace = 6042U,
            FailedToCache = 6043U,
            FailedToUncache = 6044U,
            AlreadyCached = 6045U,
            NotCached = 6046U,
            UnauthorizedCaller = 6047U,
            RainTokenMintMismatch = 6048U,
            AmountMustBeGreaterThanZero = 6049U
        }
    }

    namespace Types
    {
        public partial class AddItemEffectArgs
        {
            public ulong ItemIndex { get; set; }

            public ulong ItemClassIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public ushort ItemUsageIndex { get; set; }

            public PermissivenessType UseItemPermissivenessToUse { get; set; }

            public ulong Space { get; set; }

            public UsageInfo UsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WriteU64(ItemClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WriteU16(ItemUsageIndex, offset);
                offset += 2;
                if (UseItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UseItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(Space, offset);
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

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddItemEffectArgs result)
            {
                int offset = initialOffset;
                result = new AddItemEffectArgs();
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemUsageIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.UseItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.Space = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    offset += UsageInfo.Deserialize(_data, offset, out var resultUsageInfo);
                    result.UsageInfo = resultUsageInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class UsageInfo
        {
            public byte[][] UsageStateProof { get; set; }

            public byte[] UsageState { get; set; }

            public byte[][] UsageProof { get; set; }

            public byte[] Usage { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(UsageStateProof.Length, offset);
                offset += 4;
                foreach (var usageStateProofElement in UsageStateProof)
                {
                    _data.WriteSpan(usageStateProofElement, offset);
                    offset += usageStateProofElement.Length;
                }

                _data.WriteS32(UsageState.Length, offset);
                offset += 4;
                _data.WriteSpan(UsageState, offset);
                offset += UsageState.Length;
                _data.WriteS32(UsageProof.Length, offset);
                offset += 4;
                foreach (var usageProofElement in UsageProof)
                {
                    _data.WriteSpan(usageProofElement, offset);
                    offset += usageProofElement.Length;
                }

                _data.WriteS32(Usage.Length, offset);
                offset += 4;
                _data.WriteSpan(Usage, offset);
                offset += Usage.Length;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UsageInfo result)
            {
                int offset = initialOffset;
                result = new UsageInfo();
                int resultUsageStateProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageStateProof = new byte[resultUsageStateProofLength][];
                for (uint resultUsageStateProofIdx = 0; resultUsageStateProofIdx < resultUsageStateProofLength; resultUsageStateProofIdx++)
                {
                    result.UsageStateProof[resultUsageStateProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                int resultUsageStateLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageState = _data.GetBytes(offset, resultUsageStateLength);
                offset += resultUsageStateLength;
                int resultUsageProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.UsageProof = new byte[resultUsageProofLength][];
                for (uint resultUsageProofIdx = 0; resultUsageProofIdx < resultUsageProofLength; resultUsageProofIdx++)
                {
                    result.UsageProof[resultUsageProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                int resultUsageLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Usage = _data.GetBytes(offset, resultUsageLength);
                offset += resultUsageLength;
                return offset - initialOffset;
            }
        }

        public partial class AddItemArgs
        {
            public ulong ItemIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public ulong Amount { get; set; }

            public PermissivenessType AddItemPermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (AddItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)AddItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddItemArgs result)
            {
                int offset = initialOffset;
                result = new AddItemArgs();
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.AddItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class RemoveItemArgs
        {
            public ulong ItemIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public ulong Amount { get; set; }

            public PermissivenessType RemoveItemPermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (RemoveItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)RemoveItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RemoveItemArgs result)
            {
                int offset = initialOffset;
                result = new RemoveItemArgs();
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.RemoveItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class ToggleEquipItemArgs
        {
            public ulong ItemIndex { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public ulong Amount { get; set; }

            public bool Equipping { get; set; }

            public ushort BodyPartIndex { get; set; }

            public PermissivenessType EquipItemPermissivenessToUse { get; set; }

            public ushort ItemUsageIndex { get; set; }

            public byte[][] ItemUsageProof { get; set; }

            public byte[] ItemUsage { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WriteBool(Equipping, offset);
                offset += 1;
                _data.WriteU16(BodyPartIndex, offset);
                offset += 2;
                if (EquipItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)EquipItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU16(ItemUsageIndex, offset);
                offset += 2;
                if (ItemUsageProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ItemUsageProof.Length, offset);
                    offset += 4;
                    foreach (var itemUsageProofElement in ItemUsageProof)
                    {
                        _data.WriteSpan(itemUsageProofElement, offset);
                        offset += itemUsageProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ItemUsage != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ItemUsage.Length, offset);
                    offset += 4;
                    _data.WriteSpan(ItemUsage, offset);
                    offset += ItemUsage.Length;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ToggleEquipItemArgs result)
            {
                int offset = initialOffset;
                result = new ToggleEquipItemArgs();
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.Equipping = _data.GetBool(offset);
                offset += 1;
                result.BodyPartIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.EquipItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.ItemUsageIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    int resultItemUsageProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ItemUsageProof = new byte[resultItemUsageProofLength][];
                    for (uint resultItemUsageProofIdx = 0; resultItemUsageProofIdx < resultItemUsageProofLength; resultItemUsageProofIdx++)
                    {
                        result.ItemUsageProof[resultItemUsageProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultItemUsageLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ItemUsage = _data.GetBytes(offset, resultItemUsageLength);
                    offset += resultItemUsageLength;
                }

                return offset - initialOffset;
            }
        }

        public partial class ResetPlayerStatsArgs
        {
            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PermissivenessType EquipItemPermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                if (EquipItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)EquipItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ResetPlayerStatsArgs result)
            {
                int offset = initialOffset;
                result = new ResetPlayerStatsArgs();
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.EquipItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class DrainPlayerArgs
        {
            public ulong Index { get; set; }

            public ulong ClassIndex { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PublicKey PlayerClassMint { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                _data.WritePubKey(PlayerClassMint, offset);
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

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainPlayerArgs result)
            {
                int offset = initialOffset;
                result = new DrainPlayerArgs();
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                result.PlayerClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class UpdatePlayerClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public PlayerClassData PlayerClassData { get; set; }

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

                if (PlayerClassData != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += PlayerClassData.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdatePlayerClassArgs result)
            {
                int offset = initialOffset;
                result = new UpdatePlayerClassArgs();
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
                    offset += PlayerClassData.Deserialize(_data, offset, out var resultPlayerClassData);
                    result.PlayerClassData = resultPlayerClassData;
                }

                return offset - initialOffset;
            }
        }

        public partial class CreatePlayerClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong? ParentOfParentClassIndex { get; set; }

            public ulong Space { get; set; }

            public ushort DesiredNamespaceArraySize { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public bool StoreMint { get; set; }

            public bool StoreMetadataFields { get; set; }

            public PlayerClassData PlayerClassData { get; set; }

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
                offset += PlayerClassData.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreatePlayerClassArgs result)
            {
                int offset = initialOffset;
                result = new CreatePlayerClassArgs();
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
                offset += PlayerClassData.Deserialize(_data, offset, out var resultPlayerClassData);
                result.PlayerClassData = resultPlayerClassData;
                return offset - initialOffset;
            }
        }

        public partial class DrainPlayerClassArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public PublicKey PlayerClassMint { get; set; }

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

                _data.WritePubKey(PlayerClassMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainPlayerClassArgs result)
            {
                int offset = initialOffset;
                result = new DrainPlayerClassArgs();
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

                result.PlayerClassMint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class BuildPlayerArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong NewPlayerIndex { get; set; }

            public ulong Space { get; set; }

            public PublicKey PlayerClassMint { get; set; }

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

                _data.WriteU64(NewPlayerIndex, offset);
                offset += 8;
                _data.WriteU64(Space, offset);
                offset += 8;
                _data.WritePubKey(PlayerClassMint, offset);
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

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BuildPlayerArgs result)
            {
                int offset = initialOffset;
                result = new BuildPlayerArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.NewPlayerIndex = _data.GetU64(offset);
                offset += 8;
                result.Space = _data.GetU64(offset);
                offset += 8;
                result.PlayerClassMint = _data.GetPubKey(offset);
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

        public partial class UpdatePlayerArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PermissivenessType UpdatePermissivenessToUse { get; set; }

            public PublicKey PlayerClassMint { get; set; }

            public PlayerData NewData { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
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

                _data.WritePubKey(PlayerClassMint, offset);
                offset += 32;
                if (NewData != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += NewData.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdatePlayerArgs result)
            {
                int offset = initialOffset;
                result = new UpdatePlayerArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.UpdatePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.PlayerClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    offset += PlayerData.Deserialize(_data, offset, out var resultNewData);
                    result.NewData = resultNewData;
                }

                return offset - initialOffset;
            }
        }

        public partial class UseItemArgs
        {
            public ulong ItemClassIndex { get; set; }

            public ulong ItemIndex { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PublicKey ItemMint { get; set; }

            public byte ItemMarkerSpace { get; set; }

            public PermissivenessType UseItemPermissivenessToUse { get; set; }

            public ulong Amount { get; set; }

            public ushort ItemUsageIndex { get; set; }

            public PublicKey Target { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public byte[] ItemUsageInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ItemClassIndex, offset);
                offset += 8;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WritePubKey(ItemClassMint, offset);
                offset += 32;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU8(ItemMarkerSpace, offset);
                offset += 1;
                if (UseItemPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)UseItemPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WriteU16(ItemUsageIndex, offset);
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

                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                if (ItemUsageInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ItemUsageInfo.Length, offset);
                    offset += 4;
                    _data.WriteSpan(ItemUsageInfo, offset);
                    offset += ItemUsageInfo.Length;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UseItemArgs result)
            {
                int offset = initialOffset;
                result = new UseItemArgs();
                result.ItemClassIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemMarkerSpace = _data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.UseItemPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.ItemUsageIndex = _data.GetU16(offset);
                offset += 2;
                if (_data.GetBool(offset++))
                {
                    result.Target = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    int resultItemUsageInfoLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ItemUsageInfo = _data.GetBytes(offset, resultItemUsageInfoLength);
                    offset += resultItemUsageInfoLength;
                }

                return offset - initialOffset;
            }
        }

        public partial class UpdateValidForUseIfWarmupPassedOnItemArgs
        {
            public PublicKey ItemMint { get; set; }

            public ulong ItemIndex { get; set; }

            public ushort ItemUsageIndex { get; set; }

            public ulong ItemClassIndex { get; set; }

            public ulong Amount { get; set; }

            public PublicKey ItemClassMint { get; set; }

            public PermissivenessType UsagePermissivenessToUse { get; set; }

            public ulong Index { get; set; }

            public PublicKey PlayerMint { get; set; }

            public byte[][] ItemUsageProof { get; set; }

            public byte[] ItemUsage { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(ItemMint, offset);
                offset += 32;
                _data.WriteU64(ItemIndex, offset);
                offset += 8;
                _data.WriteU16(ItemUsageIndex, offset);
                offset += 2;
                _data.WriteU64(ItemClassIndex, offset);
                offset += 8;
                _data.WriteU64(Amount, offset);
                offset += 8;
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

                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WritePubKey(PlayerMint, offset);
                offset += 32;
                if (ItemUsageProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ItemUsageProof.Length, offset);
                    offset += 4;
                    foreach (var itemUsageProofElement in ItemUsageProof)
                    {
                        _data.WriteSpan(itemUsageProofElement, offset);
                        offset += itemUsageProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (ItemUsage != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(ItemUsage.Length, offset);
                    offset += 4;
                    _data.WriteSpan(ItemUsage, offset);
                    offset += ItemUsage.Length;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateValidForUseIfWarmupPassedOnItemArgs result)
            {
                int offset = initialOffset;
                result = new UpdateValidForUseIfWarmupPassedOnItemArgs();
                result.ItemMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemIndex = _data.GetU64(offset);
                offset += 8;
                result.ItemUsageIndex = _data.GetU16(offset);
                offset += 2;
                result.ItemClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.ItemClassMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.UsagePermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                result.Index = _data.GetU64(offset);
                offset += 8;
                result.PlayerMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    int resultItemUsageProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ItemUsageProof = new byte[resultItemUsageProofLength][];
                    for (uint resultItemUsageProofIdx = 0; resultItemUsageProofIdx < resultItemUsageProofLength; resultItemUsageProofIdx++)
                    {
                        result.ItemUsageProof[resultItemUsageProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultItemUsageLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.ItemUsage = _data.GetBytes(offset, resultItemUsageLength);
                    offset += resultItemUsageLength;
                }

                return offset - initialOffset;
            }
        }

        public partial class EquippedItem
        {
            public PublicKey Item { get; set; }

            public ulong Amount { get; set; }

            public ushort Index { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Item, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WriteU16(Index, offset);
                offset += 2;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EquippedItem result)
            {
                int offset = initialOffset;
                result = new EquippedItem();
                result.Item = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU16(offset);
                offset += 2;
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

        public partial class PlayerCategory
        {
            public string Category { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += _data.WriteBorshString(Category, offset);
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PlayerCategory result)
            {
                int offset = initialOffset;
                result = new PlayerCategory();
                offset += _data.GetBorshString(offset, out var resultCategory);
                result.Category = resultCategory;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class StatsUri
        {
            public string StatsUriField { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += _data.WriteBorshString(StatsUriField, offset);
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StatsUri result)
            {
                int offset = initialOffset;
                result = new StatsUri();
                offset += _data.GetBorshString(offset, out var resultStatsUriField);
                result.StatsUriField = resultStatsUriField;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class BodyPart
        {
            public ushort Index { get; set; }

            public string BodyPartField { get; set; }

            public ulong? TotalItemSpots { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(Index, offset);
                offset += 2;
                offset += _data.WriteBorshString(BodyPartField, offset);
                if (TotalItemSpots != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(TotalItemSpots.Value, offset);
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

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BodyPart result)
            {
                int offset = initialOffset;
                result = new BodyPart();
                result.Index = _data.GetU16(offset);
                offset += 2;
                offset += _data.GetBorshString(offset, out var resultBodyPartField);
                result.BodyPartField = resultBodyPartField;
                if (_data.GetBool(offset++))
                {
                    result.TotalItemSpots = _data.GetU64(offset);
                    offset += 8;
                }

                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class PlayerClassData
        {
            public PlayerClassSettings Settings { get; set; }

            public PlayerClassConfig Config { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += Settings.Serialize(_data, offset);
                offset += Config.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PlayerClassData result)
            {
                int offset = initialOffset;
                result = new PlayerClassData();
                offset += PlayerClassSettings.Deserialize(_data, offset, out var resultSettings);
                result.Settings = resultSettings;
                offset += PlayerClassConfig.Deserialize(_data, offset, out var resultConfig);
                result.Config = resultConfig;
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

        public partial class PlayerClassSettings
        {
            public PlayerCategory DefaultCategory { get; set; }

            public Boolean ChildrenMustBeEditions { get; set; }

            public Boolean BuilderMustBeHolder { get; set; }

            public Permissiveness[] UpdatePermissiveness { get; set; }

            public Permissiveness[] InstanceUpdatePermissiveness { get; set; }

            public Permissiveness[] BuildPermissiveness { get; set; }

            public Permissiveness[] EquipItemPermissiveness { get; set; }

            public Permissiveness[] AddItemPermissiveness { get; set; }

            public Permissiveness[] UseItemPermissiveness { get; set; }

            public Permissiveness[] UnequipItemPermissiveness { get; set; }

            public Permissiveness[] RemoveItemPermissiveness { get; set; }

            public ulong? StakingWarmUpDuration { get; set; }

            public ulong? StakingCooldownDuration { get; set; }

            public Permissiveness[] StakingPermissiveness { get; set; }

            public Permissiveness[] UnstakingPermissiveness { get; set; }

            public ChildUpdatePropagationPermissiveness[] ChildUpdatePropagationPermissiveness { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (DefaultCategory != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += DefaultCategory.Serialize(_data, offset);
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

                if (InstanceUpdatePermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(InstanceUpdatePermissiveness.Length, offset);
                    offset += 4;
                    foreach (var instanceUpdatePermissivenessElement in InstanceUpdatePermissiveness)
                    {
                        offset += instanceUpdatePermissivenessElement.Serialize(_data, offset);
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

                if (EquipItemPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(EquipItemPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var equipItemPermissivenessElement in EquipItemPermissiveness)
                    {
                        offset += equipItemPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (AddItemPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(AddItemPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var addItemPermissivenessElement in AddItemPermissiveness)
                    {
                        offset += addItemPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UseItemPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UseItemPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var useItemPermissivenessElement in UseItemPermissiveness)
                    {
                        offset += useItemPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (UnequipItemPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(UnequipItemPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var unequipItemPermissivenessElement in UnequipItemPermissiveness)
                    {
                        offset += unequipItemPermissivenessElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (RemoveItemPermissiveness != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(RemoveItemPermissiveness.Length, offset);
                    offset += 4;
                    foreach (var removeItemPermissivenessElement in RemoveItemPermissiveness)
                    {
                        offset += removeItemPermissivenessElement.Serialize(_data, offset);
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

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PlayerClassSettings result)
            {
                int offset = initialOffset;
                result = new PlayerClassSettings();
                if (_data.GetBool(offset++))
                {
                    offset += PlayerCategory.Deserialize(_data, offset, out var resultDefaultCategory);
                    result.DefaultCategory = resultDefaultCategory;
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
                    int resultInstanceUpdatePermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.InstanceUpdatePermissiveness = new Permissiveness[resultInstanceUpdatePermissivenessLength];
                    for (uint resultInstanceUpdatePermissivenessIdx = 0; resultInstanceUpdatePermissivenessIdx < resultInstanceUpdatePermissivenessLength; resultInstanceUpdatePermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultInstanceUpdatePermissivenessresultInstanceUpdatePermissivenessIdx);
                        result.InstanceUpdatePermissiveness[resultInstanceUpdatePermissivenessIdx] = resultInstanceUpdatePermissivenessresultInstanceUpdatePermissivenessIdx;
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
                    int resultEquipItemPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.EquipItemPermissiveness = new Permissiveness[resultEquipItemPermissivenessLength];
                    for (uint resultEquipItemPermissivenessIdx = 0; resultEquipItemPermissivenessIdx < resultEquipItemPermissivenessLength; resultEquipItemPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultEquipItemPermissivenessresultEquipItemPermissivenessIdx);
                        result.EquipItemPermissiveness[resultEquipItemPermissivenessIdx] = resultEquipItemPermissivenessresultEquipItemPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultAddItemPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.AddItemPermissiveness = new Permissiveness[resultAddItemPermissivenessLength];
                    for (uint resultAddItemPermissivenessIdx = 0; resultAddItemPermissivenessIdx < resultAddItemPermissivenessLength; resultAddItemPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultAddItemPermissivenessresultAddItemPermissivenessIdx);
                        result.AddItemPermissiveness[resultAddItemPermissivenessIdx] = resultAddItemPermissivenessresultAddItemPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultUseItemPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UseItemPermissiveness = new Permissiveness[resultUseItemPermissivenessLength];
                    for (uint resultUseItemPermissivenessIdx = 0; resultUseItemPermissivenessIdx < resultUseItemPermissivenessLength; resultUseItemPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultUseItemPermissivenessresultUseItemPermissivenessIdx);
                        result.UseItemPermissiveness[resultUseItemPermissivenessIdx] = resultUseItemPermissivenessresultUseItemPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultUnequipItemPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.UnequipItemPermissiveness = new Permissiveness[resultUnequipItemPermissivenessLength];
                    for (uint resultUnequipItemPermissivenessIdx = 0; resultUnequipItemPermissivenessIdx < resultUnequipItemPermissivenessLength; resultUnequipItemPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultUnequipItemPermissivenessresultUnequipItemPermissivenessIdx);
                        result.UnequipItemPermissiveness[resultUnequipItemPermissivenessIdx] = resultUnequipItemPermissivenessresultUnequipItemPermissivenessIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultRemoveItemPermissivenessLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.RemoveItemPermissiveness = new Permissiveness[resultRemoveItemPermissivenessLength];
                    for (uint resultRemoveItemPermissivenessIdx = 0; resultRemoveItemPermissivenessIdx < resultRemoveItemPermissivenessLength; resultRemoveItemPermissivenessIdx++)
                    {
                        offset += Permissiveness.Deserialize(_data, offset, out var resultRemoveItemPermissivenessresultRemoveItemPermissivenessIdx);
                        result.RemoveItemPermissiveness[resultRemoveItemPermissivenessIdx] = resultRemoveItemPermissivenessresultRemoveItemPermissivenessIdx;
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

        public partial class PlayerClassConfig
        {
            public StatsUri StartingStatsUri { get; set; }

            public BasicStatTemplate[] BasicStats { get; set; }

            public BodyPart[] BodyParts { get; set; }

            public Callback EquipValidation { get; set; }

            public Callback AddToPackValidation { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (StartingStatsUri != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += StartingStatsUri.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (BasicStats != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(BasicStats.Length, offset);
                    offset += 4;
                    foreach (var basicStatsElement in BasicStats)
                    {
                        offset += basicStatsElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (BodyParts != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(BodyParts.Length, offset);
                    offset += 4;
                    foreach (var bodyPartsElement in BodyParts)
                    {
                        offset += bodyPartsElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (EquipValidation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += EquipValidation.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (AddToPackValidation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += AddToPackValidation.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PlayerClassConfig result)
            {
                int offset = initialOffset;
                result = new PlayerClassConfig();
                if (_data.GetBool(offset++))
                {
                    offset += StatsUri.Deserialize(_data, offset, out var resultStartingStatsUri);
                    result.StartingStatsUri = resultStartingStatsUri;
                }

                if (_data.GetBool(offset++))
                {
                    int resultBasicStatsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.BasicStats = new BasicStatTemplate[resultBasicStatsLength];
                    for (uint resultBasicStatsIdx = 0; resultBasicStatsIdx < resultBasicStatsLength; resultBasicStatsIdx++)
                    {
                        offset += BasicStatTemplate.Deserialize(_data, offset, out var resultBasicStatsresultBasicStatsIdx);
                        result.BasicStats[resultBasicStatsIdx] = resultBasicStatsresultBasicStatsIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    int resultBodyPartsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.BodyParts = new BodyPart[resultBodyPartsLength];
                    for (uint resultBodyPartsIdx = 0; resultBodyPartsIdx < resultBodyPartsLength; resultBodyPartsIdx++)
                    {
                        offset += BodyPart.Deserialize(_data, offset, out var resultBodyPartsresultBodyPartsIdx);
                        result.BodyParts[resultBodyPartsIdx] = resultBodyPartsresultBodyPartsIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += Callback.Deserialize(_data, offset, out var resultEquipValidation);
                    result.EquipValidation = resultEquipValidation;
                }

                if (_data.GetBool(offset++))
                {
                    offset += Callback.Deserialize(_data, offset, out var resultAddToPackValidation);
                    result.AddToPackValidation = resultAddToPackValidation;
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

        public partial class PlayerData
        {
            public StatsUri StatsUri { get; set; }

            public PlayerCategory Category { get; set; }

            public BasicStat[] BasicStats { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (StatsUri != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += StatsUri.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Category != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += Category.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (BasicStats != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(BasicStats.Length, offset);
                    offset += 4;
                    foreach (var basicStatsElement in BasicStats)
                    {
                        offset += basicStatsElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PlayerData result)
            {
                int offset = initialOffset;
                result = new PlayerData();
                if (_data.GetBool(offset++))
                {
                    offset += StatsUri.Deserialize(_data, offset, out var resultStatsUri);
                    result.StatsUri = resultStatsUri;
                }

                if (_data.GetBool(offset++))
                {
                    offset += PlayerCategory.Deserialize(_data, offset, out var resultCategory);
                    result.Category = resultCategory;
                }

                if (_data.GetBool(offset++))
                {
                    int resultBasicStatsLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.BasicStats = new BasicStat[resultBasicStatsLength];
                    for (uint resultBasicStatsIdx = 0; resultBasicStatsIdx < resultBasicStatsLength; resultBasicStatsIdx++)
                    {
                        offset += BasicStat.Deserialize(_data, offset, out var resultBasicStatsresultBasicStatsIdx);
                        result.BasicStats[resultBasicStatsIdx] = resultBasicStatsresultBasicStatsIdx;
                    }
                }

                return offset - initialOffset;
            }
        }

        public partial class BasicStatTemplate
        {
            public ushort Index { get; set; }

            public string Name { get; set; }

            public BasicStatType StatType { get; set; }

            public InheritanceState Inherited { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(Index, offset);
                offset += 2;
                offset += _data.WriteBorshString(Name, offset);
                offset += StatType.Serialize(_data, offset);
                _data.WriteU8((byte)Inherited, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BasicStatTemplate result)
            {
                int offset = initialOffset;
                result = new BasicStatTemplate();
                result.Index = _data.GetU16(offset);
                offset += 2;
                offset += _data.GetBorshString(offset, out var resultName);
                result.Name = resultName;
                offset += BasicStatType.Deserialize(_data, offset, out var resultStatType);
                result.StatType = resultStatType;
                result.Inherited = (InheritanceState)_data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class BasicStat
        {
            public ushort Index { get; set; }

            public BasicStatState State { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU16(Index, offset);
                offset += 2;
                offset += State.Serialize(_data, offset);
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BasicStat result)
            {
                int offset = initialOffset;
                result = new BasicStat();
                result.Index = _data.GetU16(offset);
                offset += 2;
                offset += BasicStatState.Deserialize(_data, offset, out var resultState);
                result.State = resultState;
                return offset - initialOffset;
            }
        }

        public partial class Threshold
        {
            public string Name { get; set; }

            public byte Value { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += _data.WriteBorshString(Name, offset);
                _data.WriteU8(Value, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Threshold result)
            {
                int offset = initialOffset;
                result = new Threshold();
                offset += _data.GetBorshString(offset, out var resultName);
                result.Name = resultName;
                result.Value = _data.GetU8(offset);
                offset += 1;
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

        public enum PermissivenessType : byte
        {
            TokenHolder,
            ParentTokenHolder,
            UpdateAuthority,
            Anybody
        }

        public enum InheritanceState : byte
        {
            NotInherited,
            Inherited,
            Overridden
        }

        public enum ChildUpdatePropagationPermissivenessType : byte
        {
            UpdatePermissiveness,
            InstanceUpdatePermissiveness,
            BuildPermissiveness,
            ChildUpdatePropagationPermissiveness,
            ChildrenMustBeEditionsPermissiveness,
            BuilderMustBeHolderPermissiveness,
            StakingPermissiveness,
            Namespaces,
            EquipItemPermissiveness,
            AddItemPermissiveness,
            UseItemPermissiveness,
            BasicStatTemplates,
            DefaultCategory,
            BodyParts,
            StatsUri
        }

        public enum BasicStatTypeType : byte
        {
            Enum,
            Integer,
            Bool,
            String
        }

        public partial class EnumType
        {
            public byte Starting { get; set; }

            public Threshold[] Values { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EnumType result)
            {
                int offset = initialOffset;
                result = new EnumType();
                result.Starting = _data.GetU8(offset);
                offset += 1;
                int resultValuesLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Values = new Threshold[resultValuesLength];
                for (uint resultValuesIdx = 0; resultValuesIdx < resultValuesLength; resultValuesIdx++)
                {
                    offset += Threshold.Deserialize(_data, offset, out var resultValuesresultValuesIdx);
                    result.Values[resultValuesIdx] = resultValuesresultValuesIdx;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8(Starting, offset);
                offset += 1;
                _data.WriteS32(Values.Length, offset);
                offset += 4;
                foreach (var valuesElement in Values)
                {
                    offset += valuesElement.Serialize(_data, offset);
                }

                return offset - initialOffset;
            }
        }

        public partial class IntegerType
        {
            public long? Min { get; set; }

            public long? Max { get; set; }

            public long Starting { get; set; }

            public ulong? StakingAmountScaler { get; set; }

            public ulong? StakingDurationScaler { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out IntegerType result)
            {
                int offset = initialOffset;
                result = new IntegerType();
                if (_data.GetBool(offset++))
                {
                    result.Min = _data.GetS64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.Max = _data.GetS64(offset);
                    offset += 8;
                }

                result.Starting = _data.GetS64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.StakingAmountScaler = _data.GetU64(offset);
                    offset += 8;
                }

                if (_data.GetBool(offset++))
                {
                    result.StakingDurationScaler = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (Min != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS64(Min.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (Max != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS64(Max.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteS64(Starting, offset);
                offset += 8;
                if (StakingAmountScaler != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingAmountScaler.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (StakingDurationScaler != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingDurationScaler.Value, offset);
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

        public partial class BoolType
        {
            public bool Starting { get; set; }

            public ulong? StakingFlip { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BoolType result)
            {
                int offset = initialOffset;
                result = new BoolType();
                result.Starting = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.StakingFlip = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteBool(Starting, offset);
                offset += 1;
                if (StakingFlip != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(StakingFlip.Value, offset);
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

        public partial class StringType
        {
            public string Starting { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StringType result)
            {
                int offset = initialOffset;
                result = new StringType();
                offset += _data.GetBorshString(offset, out var resultStarting);
                result.Starting = resultStarting;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += _data.WriteBorshString(Starting, offset);
                return offset - initialOffset;
            }
        }

        public partial class BasicStatType
        {
            public EnumType EnumValue { get; set; }

            public IntegerType IntegerValue { get; set; }

            public BoolType BoolValue { get; set; }

            public StringType StringValue { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Type, offset);
                offset += 1;
                switch (Type)
                {
                    case BasicStatTypeType.Enum:
                        offset += EnumValue.Serialize(_data, offset);
                        break;
                    case BasicStatTypeType.Integer:
                        offset += IntegerValue.Serialize(_data, offset);
                        break;
                    case BasicStatTypeType.Bool:
                        offset += BoolValue.Serialize(_data, offset);
                        break;
                    case BasicStatTypeType.String:
                        offset += StringValue.Serialize(_data, offset);
                        break;
                }

                return offset - initialOffset;
            }

            public BasicStatTypeType Type { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BasicStatType result)
            {
                int offset = initialOffset;
                result = new BasicStatType();
                result.Type = (BasicStatTypeType)_data.GetU8(offset);
                offset += 1;
                switch (result.Type)
                {
                    case BasicStatTypeType.Enum:
                    {
                        EnumType tmpEnumValue = new EnumType();
                        offset += EnumType.Deserialize(_data, offset, out tmpEnumValue);
                        result.EnumValue = tmpEnumValue;
                        break;
                    }

                    case BasicStatTypeType.Integer:
                    {
                        IntegerType tmpIntegerValue = new IntegerType();
                        offset += IntegerType.Deserialize(_data, offset, out tmpIntegerValue);
                        result.IntegerValue = tmpIntegerValue;
                        break;
                    }

                    case BasicStatTypeType.Bool:
                    {
                        BoolType tmpBoolValue = new BoolType();
                        offset += BoolType.Deserialize(_data, offset, out tmpBoolValue);
                        result.BoolValue = tmpBoolValue;
                        break;
                    }

                    case BasicStatTypeType.String:
                    {
                        StringType tmpStringValue = new StringType();
                        offset += StringType.Deserialize(_data, offset, out tmpStringValue);
                        result.StringValue = tmpStringValue;
                        break;
                    }
                }

                return offset - initialOffset;
            }
        }

        public enum BasicStatStateType : byte
        {
            Enum,
            Integer,
            Bool,
            String,
            Null
        }

        public partial class EnumType
        {
            public byte Current { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EnumType result)
            {
                int offset = initialOffset;
                result = new EnumType();
                result.Current = _data.GetU8(offset);
                offset += 1;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8(Current, offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class IntegerType
        {
            public long Base { get; set; }

            public long WithTemporaryChanges { get; set; }

            public long TemporaryNumerator { get; set; }

            public long TemporaryDenominator { get; set; }

            public long Finalized { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out IntegerType result)
            {
                int offset = initialOffset;
                result = new IntegerType();
                result.Base = _data.GetS64(offset);
                offset += 8;
                result.WithTemporaryChanges = _data.GetS64(offset);
                offset += 8;
                result.TemporaryNumerator = _data.GetS64(offset);
                offset += 8;
                result.TemporaryDenominator = _data.GetS64(offset);
                offset += 8;
                result.Finalized = _data.GetS64(offset);
                offset += 8;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS64(Base, offset);
                offset += 8;
                _data.WriteS64(WithTemporaryChanges, offset);
                offset += 8;
                _data.WriteS64(TemporaryNumerator, offset);
                offset += 8;
                _data.WriteS64(TemporaryDenominator, offset);
                offset += 8;
                _data.WriteS64(Finalized, offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class BoolType
        {
            public bool Current { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BoolType result)
            {
                int offset = initialOffset;
                result = new BoolType();
                result.Current = _data.GetBool(offset);
                offset += 1;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteBool(Current, offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class StringType
        {
            public string Current { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out StringType result)
            {
                int offset = initialOffset;
                result = new StringType();
                offset += _data.GetBorshString(offset, out var resultCurrent);
                result.Current = resultCurrent;
                return offset - initialOffset;
            }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                offset += _data.WriteBorshString(Current, offset);
                return offset - initialOffset;
            }
        }

        public partial class BasicStatState
        {
            public EnumType EnumValue { get; set; }

            public IntegerType IntegerValue { get; set; }

            public BoolType BoolValue { get; set; }

            public StringType StringValue { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Type, offset);
                offset += 1;
                switch (Type)
                {
                    case BasicStatStateType.Enum:
                        offset += EnumValue.Serialize(_data, offset);
                        break;
                    case BasicStatStateType.Integer:
                        offset += IntegerValue.Serialize(_data, offset);
                        break;
                    case BasicStatStateType.Bool:
                        offset += BoolValue.Serialize(_data, offset);
                        break;
                    case BasicStatStateType.String:
                        offset += StringValue.Serialize(_data, offset);
                        break;
                }

                return offset - initialOffset;
            }

            public BasicStatStateType Type { get; set; }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BasicStatState result)
            {
                int offset = initialOffset;
                result = new BasicStatState();
                result.Type = (BasicStatStateType)_data.GetU8(offset);
                offset += 1;
                switch (result.Type)
                {
                    case BasicStatStateType.Enum:
                    {
                        EnumType tmpEnumValue = new EnumType();
                        offset += EnumType.Deserialize(_data, offset, out tmpEnumValue);
                        result.EnumValue = tmpEnumValue;
                        break;
                    }

                    case BasicStatStateType.Integer:
                    {
                        IntegerType tmpIntegerValue = new IntegerType();
                        offset += IntegerType.Deserialize(_data, offset, out tmpIntegerValue);
                        result.IntegerValue = tmpIntegerValue;
                        break;
                    }

                    case BasicStatStateType.Bool:
                    {
                        BoolType tmpBoolValue = new BoolType();
                        offset += BoolType.Deserialize(_data, offset, out tmpBoolValue);
                        result.BoolValue = tmpBoolValue;
                        break;
                    }

                    case BasicStatStateType.String:
                    {
                        StringType tmpStringValue = new StringType();
                        offset += StringType.Deserialize(_data, offset, out tmpStringValue);
                        result.StringValue = tmpStringValue;
                        break;
                    }
                }

                return offset - initialOffset;
            }
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
    }

    public partial class RaindropsPlayerClient : TransactionalBaseClient<RaindropsPlayerErrorKind>
    {
        public RaindropsPlayerClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerClass>>> GetPlayerClasssAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = PlayerClass.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerClass>>(res);
            List<PlayerClass> resultingAccounts = new List<PlayerClass>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerClass.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerClass>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Player>>> GetPlayersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = Player.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Player>>(res);
            List<Player> resultingAccounts = new List<Player>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Player.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Player>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerItemActivationMarker>>> GetPlayerItemActivationMarkersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = PlayerItemActivationMarker.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerItemActivationMarker>>(res);
            List<PlayerItemActivationMarker> resultingAccounts = new List<PlayerItemActivationMarker>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerItemActivationMarker.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerItemActivationMarker>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<PlayerClass>> GetPlayerClassAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerClass>(res);
            var resultingAccount = PlayerClass.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerClass>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<Player>> GetPlayerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<Player>(res);
            var resultingAccount = Player.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<Player>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<PlayerItemActivationMarker>> GetPlayerItemActivationMarkerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerItemActivationMarker>(res);
            var resultingAccount = PlayerItemActivationMarker.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerItemActivationMarker>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribePlayerClassAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, PlayerClass> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                PlayerClass parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = PlayerClass.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribePlayerAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, Player> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Player parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Player.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribePlayerItemActivationMarkerAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, PlayerItemActivationMarker> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                PlayerItemActivationMarker parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = PlayerItemActivationMarker.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendCreatePlayerClassAsync(CreatePlayerClassAccounts accounts, CreatePlayerClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.CreatePlayerClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdatePlayerClassAsync(UpdatePlayerClassAccounts accounts, UpdatePlayerClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.UpdatePlayerClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainPlayerClassAsync(DrainPlayerClassAccounts accounts, DrainPlayerClassArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.DrainPlayerClass(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainPlayerAsync(DrainPlayerAccounts accounts, DrainPlayerArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.DrainPlayer(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdatePlayerAsync(UpdatePlayerAccounts accounts, UpdatePlayerArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.UpdatePlayer(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBuildPlayerAsync(BuildPlayerAccounts accounts, BuildPlayerArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.BuildPlayer(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendAddItemAsync(AddItemAccounts accounts, AddItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.AddItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRemoveItemAsync(RemoveItemAccounts accounts, RemoveItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.RemoveItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUseItemAsync(UseItemAccounts accounts, UseItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.UseItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateValidForUseIfWarmupPassedOnItemAsync(UpdateValidForUseIfWarmupPassedOnItemAccounts accounts, UpdateValidForUseIfWarmupPassedOnItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.UpdateValidForUseIfWarmupPassedOnItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSubtractItemEffectAsync(SubtractItemEffectAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.SubtractItemEffect(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendAddItemEffectAsync(AddItemEffectAccounts accounts, AddItemEffectArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.AddItemEffect(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendToggleEquipItemAsync(ToggleEquipItemAccounts accounts, ToggleEquipItemArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.ToggleEquipItem(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendResetPlayerStatsAsync(ResetPlayerStatsAccounts accounts, ResetPlayerStatsArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.ResetPlayerStats(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendPlayerArtifactJoinNamespaceAsync(PlayerArtifactJoinNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.PlayerArtifactJoinNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendPlayerArtifactLeaveNamespaceAsync(PlayerArtifactLeaveNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.PlayerArtifactLeaveNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendPlayerArtifactCacheNamespaceAsync(PlayerArtifactCacheNamespaceAccounts accounts, ulong page, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.PlayerArtifactCacheNamespace(accounts, page, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendPlayerArtifactUncacheNamespaceAsync(PlayerArtifactUncacheNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsPlayerProgram.PlayerArtifactUncacheNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<RaindropsPlayerErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<RaindropsPlayerErrorKind>>{{6000U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.IncorrectOwner, "Account does not have correct owner!")}, {6001U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.Uninitialized, "Account is not initialized!")}, {6002U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.MintMismatch, "Mint Mismatch!")}, {6003U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.TokenTransferFailed, "Token transfer failed")}, {6004U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NumericalOverflowError, "Numerical overflow error")}, {6005U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.TokenMintToFailed, "Token mint to failed")}, {6006U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.TokenBurnFailed, "TokenBurnFailed")}, {6007U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.DerivedKeyInvalid, "Derived key is invalid")}, {6008U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NoParentPresent, "No parent present")}, {6009U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.ExpectedParent, "Expected parent")}, {6010U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.ChildrenStillExist, "You need to kill the children before killing the parent")}, {6011U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.UnstakeTokensFirst, "Unstake tokens first")}, {6012U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.MustBeHolderToBuild, "Must be holder to build")}, {6013U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotRemoveThisMuch, "Cannot remove this much of this item because there is not enough of it or too much of it is equipped")}, {6014U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.UsageRootNotPresent, "This item lacks a usage root")}, {6015U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.InvalidProof, "Invalid proof")}, {6016U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.ItemContainsNoUsages, "Item contains no usages")}, {6017U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.FoundNoMatchingUsage, "Found no item usage matching this index")}, {6018U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotEquipConsumable, "Cannot equip consumable")}, {6019U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.BodyPartNotEligible, "This body part cannot equip this item")}, {6020U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotUnequipThisMuch, "Cannot unequip this much")}, {6021U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.BodyPartContainsTooManyOfThisType, "Body part contains too many items of this type on it")}, {6022U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.BodyPartContainsTooMany, "Body part contains too many items")}, {6023U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotEquipItemWithoutUsageOrMerkle, "Cannot equip item without usage or merkle")}, {6024U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NoBodyPartsToEquip, "No body parts to equip")}, {6025U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.UnableToFindBodyPartByIndex, "Unable to find body part with this index")}, {6026U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.ItemCannotBePairedWithSelf, "Item cannot be paired with self")}, {6027U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.ItemCannotBeEquippedWithDNPEntry, "Item cannot be equipped because a DNP entry is also equipped")}, {6028U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.BasicStatTemplateTypeDoesNotMatchBasicStatType, "Template stat type does not match stat of player, try updating player permissionlessly before running this command again")}, {6029U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotAlterThisTypeNumerically, "Cannot numerically alter this type of stat")}, {6030U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.Unreachable, "Unreachable code")}, {6031U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NotValidForUseYet, "Not valid for use yet")}, {6032U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.RemoveEquipmentFirst, "Remove equipped items first")}, {6033U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.DeactivateAllItemsFirst, "Deactivate all items first")}, {6034U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.RemoveAllItemsFromBackpackFirst, "Remove all items from backpack first")}, {6035U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.InsufficientBalance, "Insufficient Balance")}, {6036U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.PermissivenessNotFound, "Permissiveness Not Found")}, {6037U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.MustSpecifyPermissivenessType, "Must specify permissiveness type")}, {6038U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.IndexAlreadyUsed, "Cannot use the same index in basic stats or body parts twice")}, {6039U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NameAlreadyUsed, "Cannot use the same name in basic stats or body parts twice")}, {6040U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.CannotResetPlayerStatsUntilItemEffectsAreRemoved, "Cannot reset player until item effects removed")}, {6041U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.FailedToJoinNamespace, "Failed to join namespace")}, {6042U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.FailedToLeaveNamespace, "Failed to leave namespace")}, {6043U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.FailedToCache, "Failed to cache")}, {6044U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.FailedToUncache, "Failed to uncache")}, {6045U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.AlreadyCached, "Already cached")}, {6046U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.NotCached, "Not cached")}, {6047U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.UnauthorizedCaller, "Unauthorized Caller")}, {6048U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.RainTokenMintMismatch, "Rain token mint mismatch")}, {6049U, new ProgramError<RaindropsPlayerErrorKind>(RaindropsPlayerErrorKind.AmountMustBeGreaterThanZero, "Amount must be greater than zero")}, };
        }
    }

    namespace Program
    {
        public class CreatePlayerClassAccounts
        {
            public PublicKey PlayerClass { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey Edition { get; set; }

            public PublicKey Parent { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class UpdatePlayerClassAccounts
        {
            public PublicKey PlayerClass { get; set; }

            public PublicKey PlayerMint { get; set; }

            public PublicKey Parent { get; set; }
        }

        public class DrainPlayerClassAccounts
        {
            public PublicKey PlayerClass { get; set; }

            public PublicKey ParentClass { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class DrainPlayerAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey Receiver { get; set; }

            public PublicKey RainToken { get; set; }

            public PublicKey RainTokenProgramAccount { get; set; }

            public PublicKey TokenProgram { get; set; }
        }

        public class UpdatePlayerAccounts
        {
            public PublicKey PlayerClass { get; set; }

            public PublicKey Player { get; set; }
        }

        public class BuildPlayerAccounts
        {
            public PublicKey PlayerClass { get; set; }

            public PublicKey NewPlayer { get; set; }

            public PublicKey NewPlayerMint { get; set; }

            public PublicKey NewPlayerMetadata { get; set; }

            public PublicKey NewPlayerEdition { get; set; }

            public PublicKey NewPlayerToken { get; set; }

            public PublicKey RainTokenTransferAuthority { get; set; }

            public PublicKey RainToken { get; set; }

            public PublicKey RainTokenMint { get; set; }

            public PublicKey RainTokenProgramAccount { get; set; }

            public PublicKey NewPlayerTokenHolder { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class AddItemAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemTransferAuthority { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey ValidationProgram { get; set; }
        }

        public class RemoveItemAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey ItemAccount { get; set; }

            public PublicKey ItemAccountOwner { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey AssociatedTokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey ValidationProgram { get; set; }
        }

        public class UseItemAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey ItemProgram { get; set; }

            public PublicKey Clock { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey ValidationProgram { get; set; }
        }

        public class UpdateValidForUseIfWarmupPassedOnItemAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey ItemProgram { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class SubtractItemEffectAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey PlayerItemActivationMarker { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey Receiver { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class AddItemEffectAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey ItemMint { get; set; }

            public PublicKey PlayerItemActivationMarker { get; set; }

            public PublicKey ItemActivationMarker { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey ItemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey CallbackProgram { get; set; }
        }

        public class ToggleEquipItemAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }

            public PublicKey Item { get; set; }

            public PublicKey ItemClass { get; set; }

            public PublicKey PlayerItemAccount { get; set; }

            public PublicKey ValidationProgram { get; set; }
        }

        public class ResetPlayerStatsAccounts
        {
            public PublicKey Player { get; set; }

            public PublicKey PlayerClass { get; set; }
        }

        public class PlayerArtifactJoinNamespaceAccounts
        {
            public PublicKey PlayerArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class PlayerArtifactLeaveNamespaceAccounts
        {
            public PublicKey PlayerArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class PlayerArtifactCacheNamespaceAccounts
        {
            public PublicKey PlayerArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class PlayerArtifactUncacheNamespaceAccounts
        {
            public PublicKey PlayerArtifact { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public static class RaindropsPlayerProgram
        {
            public static Solana.Unity.Rpc.Models.TransactionInstruction CreatePlayerClass(CreatePlayerClassAccounts accounts, CreatePlayerClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Edition, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Parent, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(17252430632032556630UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdatePlayerClass(UpdatePlayerClassAccounts accounts, UpdatePlayerClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Parent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(586596081596355671UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainPlayerClass(DrainPlayerClassAccounts accounts, DrainPlayerClassArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ParentClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Receiver, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16856764876070927808UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainPlayer(DrainPlayerAccounts accounts, DrainPlayerArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.RainToken, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.RainTokenProgramAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1645450720548861576UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdatePlayer(UpdatePlayerAccounts accounts, UpdatePlayerArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(9083936632088948156UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction BuildPlayer(BuildPlayerAccounts accounts, BuildPlayerArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.NewPlayer, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewPlayerMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewPlayerMetadata, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewPlayerEdition, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewPlayerToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RainTokenTransferAuthority, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RainToken, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.RainTokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.RainTokenProgramAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.NewPlayerTokenHolder, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16921744861353497262UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction AddItem(AddItemAccounts accounts, AddItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemTransferAuthority, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(4148816313077147361UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction RemoveItem(RemoveItemAccounts accounts, RemoveItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemAccountOwner, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.AssociatedTokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13698474529635722244UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UseItem(UseItemAccounts accounts, UseItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14397049230667502886UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateValidForUseIfWarmupPassedOnItem(UpdateValidForUseIfWarmupPassedOnItemAccounts accounts, UpdateValidForUseIfWarmupPassedOnItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(17907134114939096589UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction SubtractItemEffect(SubtractItemEffectAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Receiver, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11309463269374151342UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction AddItemEffect(AddItemEffectAccounts accounts, AddItemEffectArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemActivationMarker, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.CallbackProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16915263429666595393UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ToggleEquipItem(ToggleEquipItemAccounts accounts, ToggleEquipItemArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Item, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerItemAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(972093238458118356UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction ResetPlayerStats(ResetPlayerStatsAccounts accounts, ResetPlayerStatsArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Player, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerClass, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(5607495533386129995UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction PlayerArtifactJoinNamespace(PlayerArtifactJoinNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13309450205267318861UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction PlayerArtifactLeaveNamespace(PlayerArtifactLeaveNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14525080557467362575UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction PlayerArtifactCacheNamespace(PlayerArtifactCacheNamespaceAccounts accounts, ulong page, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(3537984515967623308UL, offset);
                offset += 8;
                _data.WriteU64(page, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction PlayerArtifactUncacheNamespace(PlayerArtifactUncacheNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.PlayerArtifact, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1393446149022025981UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }
}