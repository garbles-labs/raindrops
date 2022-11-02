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
using RaindropsMatches;
using RaindropsMatches.Program;
using RaindropsMatches.Errors;
using RaindropsMatches.Accounts;
using RaindropsMatches.Types;

namespace RaindropsMatches
{
    namespace Accounts
    {
        public partial class Match
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 11728560967303905260UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{236, 63, 169, 38, 15, 56, 196, 162};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "gWufnjtJnc9";
            public NamespaceAndIndex[] Namespaces { get; set; }

            public PublicKey WinOracle { get; set; }

            public ulong WinOracleCooldown { get; set; }

            public ulong LastOracleCheck { get; set; }

            public PublicKey Authority { get; set; }

            public MatchState State { get; set; }

            public bool LeaveAllowed { get; set; }

            public ulong? MinimumAllowedEntryTime { get; set; }

            public byte Bump { get; set; }

            public ulong CurrentTokenTransferIndex { get; set; }

            public ulong TokenTypesAdded { get; set; }

            public ulong TokenTypesRemoved { get; set; }

            public TokenValidation[] TokenEntryValidation { get; set; }

            public Root TokenEntryValidationRoot { get; set; }

            public bool JoinAllowedDuringStart { get; set; }

            public static Match Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                Match result = new Match();
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

                result.WinOracle = _data.GetPubKey(offset);
                offset += 32;
                result.WinOracleCooldown = _data.GetU64(offset);
                offset += 8;
                result.LastOracleCheck = _data.GetU64(offset);
                offset += 8;
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.State = (MatchState)_data.GetU8(offset);
                offset += 1;
                result.LeaveAllowed = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.MinimumAllowedEntryTime = _data.GetU64(offset);
                    offset += 8;
                }

                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.CurrentTokenTransferIndex = _data.GetU64(offset);
                offset += 8;
                result.TokenTypesAdded = _data.GetU64(offset);
                offset += 8;
                result.TokenTypesRemoved = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    int resultTokenEntryValidationLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenEntryValidation = new TokenValidation[resultTokenEntryValidationLength];
                    for (uint resultTokenEntryValidationIdx = 0; resultTokenEntryValidationIdx < resultTokenEntryValidationLength; resultTokenEntryValidationIdx++)
                    {
                        offset += TokenValidation.Deserialize(_data, offset, out var resultTokenEntryValidationresultTokenEntryValidationIdx);
                        result.TokenEntryValidation[resultTokenEntryValidationIdx] = resultTokenEntryValidationresultTokenEntryValidationIdx;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultTokenEntryValidationRoot);
                    result.TokenEntryValidationRoot = resultTokenEntryValidationRoot;
                }

                result.JoinAllowedDuringStart = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }

        public partial class PlayerWinCallbackBitmap
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 10878331929741435552UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{160, 34, 7, 21, 65, 153, 247, 150};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "TnVR6KdyEZ3";
            public PublicKey MatchKey { get; set; }

            public static PlayerWinCallbackBitmap Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                PlayerWinCallbackBitmap result = new PlayerWinCallbackBitmap();
                result.MatchKey = _data.GetPubKey(offset);
                offset += 32;
                return result;
            }
        }

        public partial class WinOracle
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 14671449101079023115UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{11, 14, 10, 138, 57, 117, 155, 203};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "2rFG8UxJnKk";
            public bool Finalized { get; set; }

            public Root TokenTransferRoot { get; set; }

            public TokenDelta[] TokenTransfers { get; set; }

            public static WinOracle Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                WinOracle result = new WinOracle();
                result.Finalized = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultTokenTransferRoot);
                    result.TokenTransferRoot = resultTokenTransferRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultTokenTransfersLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenTransfers = new TokenDelta[resultTokenTransfersLength];
                    for (uint resultTokenTransfersIdx = 0; resultTokenTransfersIdx < resultTokenTransfersLength; resultTokenTransfersIdx++)
                    {
                        offset += TokenDelta.Deserialize(_data, offset, out var resultTokenTransfersresultTokenTransfersIdx);
                        result.TokenTransfers[resultTokenTransfersIdx] = resultTokenTransfersresultTokenTransfersIdx;
                    }
                }

                return result;
            }
        }
    }

    namespace Errors
    {
        public enum RaindropsMatchesErrorKind : uint
        {
            IncorrectOwner = 6000U,
            Uninitialized = 6001U,
            MintMismatch = 6002U,
            TokenTransferFailed = 6003U,
            NumericalOverflowError = 6004U,
            TokenMintToFailed = 6005U,
            TokenBurnFailed = 6006U,
            DerivedKeyInvalid = 6007U,
            InvalidStartingMatchState = 6008U,
            InvalidUpdateMatchState = 6009U,
            InvalidOracleUpdate = 6010U,
            CannotDrainYet = 6011U,
            CannotLeaveMatch = 6012U,
            ReceiverMustBeSigner = 6013U,
            PublicKeyMismatch = 6014U,
            AtaShouldNotHaveDelegate = 6015U,
            CannotEnterMatch = 6016U,
            InvalidProof = 6017U,
            RootNotPresent = 6018U,
            MustPassUpObject = 6019U,
            NoValidValidationFound = 6020U,
            Blacklisted = 6021U,
            NoTokensAllowed = 6022U,
            InvalidValidation = 6023U,
            NoDeltasFound = 6024U,
            UsePlayerEndpoint = 6025U,
            FromDoesNotMatch = 6026U,
            CannotDeltaMoreThanAmountPresent = 6027U,
            DeltaMintDoesNotMatch = 6028U,
            DestinationMismatch = 6029U,
            MatchMustBeInFinalized = 6030U,
            AtaDelegateMismatch = 6031U,
            OracleAlreadyFinalized = 6032U,
            OracleCooldownNotPassed = 6033U,
            MatchMustBeDrained = 6034U,
            NoParentPresent = 6035U,
            ReinitializationDetected = 6036U,
            FailedToLeaveNamespace = 6037U,
            FailedToJoinNamespace = 6038U,
            UnauthorizedCaller = 6039U,
            FailedToCache = 6040U,
            FailedToUncache = 6041U,
            AlreadyCached = 6042U,
            NotCached = 6043U
        }
    }

    namespace Types
    {
        public partial class CreateOrUpdateOracleArgs
        {
            public Root TokenTransferRoot { get; set; }

            public TokenDelta[] TokenTransfers { get; set; }

            public PublicKey Seed { get; set; }

            public ulong Space { get; set; }

            public bool Finalized { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (TokenTransferRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += TokenTransferRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (TokenTransfers != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(TokenTransfers.Length, offset);
                    offset += 4;
                    foreach (var tokenTransfersElement in TokenTransfers)
                    {
                        offset += tokenTransfersElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WritePubKey(Seed, offset);
                offset += 32;
                _data.WriteU64(Space, offset);
                offset += 8;
                _data.WriteBool(Finalized, offset);
                offset += 1;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateOrUpdateOracleArgs result)
            {
                int offset = initialOffset;
                result = new CreateOrUpdateOracleArgs();
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultTokenTransferRoot);
                    result.TokenTransferRoot = resultTokenTransferRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultTokenTransfersLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenTransfers = new TokenDelta[resultTokenTransfersLength];
                    for (uint resultTokenTransfersIdx = 0; resultTokenTransfersIdx < resultTokenTransfersLength; resultTokenTransfersIdx++)
                    {
                        offset += TokenDelta.Deserialize(_data, offset, out var resultTokenTransfersresultTokenTransfersIdx);
                        result.TokenTransfers[resultTokenTransfersIdx] = resultTokenTransfersresultTokenTransfersIdx;
                    }
                }

                result.Seed = _data.GetPubKey(offset);
                offset += 32;
                result.Space = _data.GetU64(offset);
                offset += 8;
                result.Finalized = _data.GetBool(offset);
                offset += 1;
                return offset - initialOffset;
            }
        }

        public partial class DrainOracleArgs
        {
            public PublicKey Seed { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(Seed, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DrainOracleArgs result)
            {
                int offset = initialOffset;
                result = new DrainOracleArgs();
                result.Seed = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class CreateMatchArgs
        {
            public MatchState MatchState { get; set; }

            public Root TokenEntryValidationRoot { get; set; }

            public TokenValidation[] TokenEntryValidation { get; set; }

            public PublicKey WinOracle { get; set; }

            public ulong WinOracleCooldown { get; set; }

            public PublicKey Authority { get; set; }

            public ulong Space { get; set; }

            public bool LeaveAllowed { get; set; }

            public bool JoinAllowedDuringStart { get; set; }

            public ulong? MinimumAllowedEntryTime { get; set; }

            public ulong DesiredNamespaceArraySize { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)MatchState, offset);
                offset += 1;
                if (TokenEntryValidationRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += TokenEntryValidationRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (TokenEntryValidation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(TokenEntryValidation.Length, offset);
                    offset += 4;
                    foreach (var tokenEntryValidationElement in TokenEntryValidation)
                    {
                        offset += tokenEntryValidationElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WritePubKey(WinOracle, offset);
                offset += 32;
                _data.WriteU64(WinOracleCooldown, offset);
                offset += 8;
                _data.WritePubKey(Authority, offset);
                offset += 32;
                _data.WriteU64(Space, offset);
                offset += 8;
                _data.WriteBool(LeaveAllowed, offset);
                offset += 1;
                _data.WriteBool(JoinAllowedDuringStart, offset);
                offset += 1;
                if (MinimumAllowedEntryTime != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(MinimumAllowedEntryTime.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(DesiredNamespaceArraySize, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateMatchArgs result)
            {
                int offset = initialOffset;
                result = new CreateMatchArgs();
                result.MatchState = (MatchState)_data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultTokenEntryValidationRoot);
                    result.TokenEntryValidationRoot = resultTokenEntryValidationRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultTokenEntryValidationLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenEntryValidation = new TokenValidation[resultTokenEntryValidationLength];
                    for (uint resultTokenEntryValidationIdx = 0; resultTokenEntryValidationIdx < resultTokenEntryValidationLength; resultTokenEntryValidationIdx++)
                    {
                        offset += TokenValidation.Deserialize(_data, offset, out var resultTokenEntryValidationresultTokenEntryValidationIdx);
                        result.TokenEntryValidation[resultTokenEntryValidationIdx] = resultTokenEntryValidationresultTokenEntryValidationIdx;
                    }
                }

                result.WinOracle = _data.GetPubKey(offset);
                offset += 32;
                result.WinOracleCooldown = _data.GetU64(offset);
                offset += 8;
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.Space = _data.GetU64(offset);
                offset += 8;
                result.LeaveAllowed = _data.GetBool(offset);
                offset += 1;
                result.JoinAllowedDuringStart = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.MinimumAllowedEntryTime = _data.GetU64(offset);
                    offset += 8;
                }

                result.DesiredNamespaceArraySize = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class UpdateMatchArgs
        {
            public MatchState MatchState { get; set; }

            public Root TokenEntryValidationRoot { get; set; }

            public TokenValidation[] TokenEntryValidation { get; set; }

            public ulong WinOracleCooldown { get; set; }

            public PublicKey Authority { get; set; }

            public bool LeaveAllowed { get; set; }

            public bool JoinAllowedDuringStart { get; set; }

            public ulong? MinimumAllowedEntryTime { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)MatchState, offset);
                offset += 1;
                if (TokenEntryValidationRoot != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += TokenEntryValidationRoot.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (TokenEntryValidation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(TokenEntryValidation.Length, offset);
                    offset += 4;
                    foreach (var tokenEntryValidationElement in TokenEntryValidation)
                    {
                        offset += tokenEntryValidationElement.Serialize(_data, offset);
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU64(WinOracleCooldown, offset);
                offset += 8;
                _data.WritePubKey(Authority, offset);
                offset += 32;
                _data.WriteBool(LeaveAllowed, offset);
                offset += 1;
                _data.WriteBool(JoinAllowedDuringStart, offset);
                offset += 1;
                if (MinimumAllowedEntryTime != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(MinimumAllowedEntryTime.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateMatchArgs result)
            {
                int offset = initialOffset;
                result = new UpdateMatchArgs();
                result.MatchState = (MatchState)_data.GetU8(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    offset += Root.Deserialize(_data, offset, out var resultTokenEntryValidationRoot);
                    result.TokenEntryValidationRoot = resultTokenEntryValidationRoot;
                }

                if (_data.GetBool(offset++))
                {
                    int resultTokenEntryValidationLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenEntryValidation = new TokenValidation[resultTokenEntryValidationLength];
                    for (uint resultTokenEntryValidationIdx = 0; resultTokenEntryValidationIdx < resultTokenEntryValidationLength; resultTokenEntryValidationIdx++)
                    {
                        offset += TokenValidation.Deserialize(_data, offset, out var resultTokenEntryValidationresultTokenEntryValidationIdx);
                        result.TokenEntryValidation[resultTokenEntryValidationIdx] = resultTokenEntryValidationresultTokenEntryValidationIdx;
                    }
                }

                result.WinOracleCooldown = _data.GetU64(offset);
                offset += 8;
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.LeaveAllowed = _data.GetBool(offset);
                offset += 1;
                result.JoinAllowedDuringStart = _data.GetBool(offset);
                offset += 1;
                if (_data.GetBool(offset++))
                {
                    result.MinimumAllowedEntryTime = _data.GetU64(offset);
                    offset += 8;
                }

                return offset - initialOffset;
            }
        }

        public partial class JoinMatchArgs
        {
            public ulong Amount { get; set; }

            public byte[][] TokenEntryValidationProof { get; set; }

            public TokenValidation TokenEntryValidation { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Amount, offset);
                offset += 8;
                if (TokenEntryValidationProof != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteS32(TokenEntryValidationProof.Length, offset);
                    offset += 4;
                    foreach (var tokenEntryValidationProofElement in TokenEntryValidationProof)
                    {
                        _data.WriteSpan(tokenEntryValidationProofElement, offset);
                        offset += tokenEntryValidationProofElement.Length;
                    }
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (TokenEntryValidation != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += TokenEntryValidation.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out JoinMatchArgs result)
            {
                int offset = initialOffset;
                result = new JoinMatchArgs();
                result.Amount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    int resultTokenEntryValidationProofLength = (int)_data.GetU32(offset);
                    offset += 4;
                    result.TokenEntryValidationProof = new byte[resultTokenEntryValidationProofLength][];
                    for (uint resultTokenEntryValidationProofIdx = 0; resultTokenEntryValidationProofIdx < resultTokenEntryValidationProofLength; resultTokenEntryValidationProofIdx++)
                    {
                        result.TokenEntryValidationProof[resultTokenEntryValidationProofIdx] = _data.GetBytes(offset, 32);
                        offset += 32;
                    }
                }

                if (_data.GetBool(offset++))
                {
                    offset += TokenValidation.Deserialize(_data, offset, out var resultTokenEntryValidation);
                    result.TokenEntryValidation = resultTokenEntryValidation;
                }

                return offset - initialOffset;
            }
        }

        public partial class LeaveMatchArgs
        {
            public ulong Amount { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(Amount, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out LeaveMatchArgs result)
            {
                int offset = initialOffset;
                result = new LeaveMatchArgs();
                result.Amount = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class DisburseTokensByOracleArgs
        {
            public TokenDeltaProofInfo TokenDeltaProofInfo { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                if (TokenDeltaProofInfo != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    offset += TokenDeltaProofInfo.Serialize(_data, offset);
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DisburseTokensByOracleArgs result)
            {
                int offset = initialOffset;
                result = new DisburseTokensByOracleArgs();
                if (_data.GetBool(offset++))
                {
                    offset += TokenDeltaProofInfo.Deserialize(_data, offset, out var resultTokenDeltaProofInfo);
                    result.TokenDeltaProofInfo = resultTokenDeltaProofInfo;
                }

                return offset - initialOffset;
            }
        }

        public partial class TokenDeltaProofInfo
        {
            public byte[][] TokenDeltaProof { get; set; }

            public TokenDelta TokenDelta { get; set; }

            public byte[][] TotalProof { get; set; }

            public ulong Total { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteS32(TokenDeltaProof.Length, offset);
                offset += 4;
                foreach (var tokenDeltaProofElement in TokenDeltaProof)
                {
                    _data.WriteSpan(tokenDeltaProofElement, offset);
                    offset += tokenDeltaProofElement.Length;
                }

                offset += TokenDelta.Serialize(_data, offset);
                _data.WriteS32(TotalProof.Length, offset);
                offset += 4;
                foreach (var totalProofElement in TotalProof)
                {
                    _data.WriteSpan(totalProofElement, offset);
                    offset += totalProofElement.Length;
                }

                _data.WriteU64(Total, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TokenDeltaProofInfo result)
            {
                int offset = initialOffset;
                result = new TokenDeltaProofInfo();
                int resultTokenDeltaProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.TokenDeltaProof = new byte[resultTokenDeltaProofLength][];
                for (uint resultTokenDeltaProofIdx = 0; resultTokenDeltaProofIdx < resultTokenDeltaProofLength; resultTokenDeltaProofIdx++)
                {
                    result.TokenDeltaProof[resultTokenDeltaProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                offset += TokenDelta.Deserialize(_data, offset, out var resultTokenDelta);
                result.TokenDelta = resultTokenDelta;
                int resultTotalProofLength = (int)_data.GetU32(offset);
                offset += 4;
                result.TotalProof = new byte[resultTotalProofLength][];
                for (uint resultTotalProofIdx = 0; resultTotalProofIdx < resultTotalProofLength; resultTotalProofIdx++)
                {
                    result.TotalProof[resultTotalProofIdx] = _data.GetBytes(offset, 32);
                    offset += 32;
                }

                result.Total = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class Root
        {
            public byte[] RootField { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteSpan(RootField, offset);
                offset += RootField.Length;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Root result)
            {
                int offset = initialOffset;
                result = new Root();
                result.RootField = _data.GetBytes(offset, 32);
                offset += 32;
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

        public partial class TokenDelta
        {
            public PublicKey From { get; set; }

            public PublicKey To { get; set; }

            public TokenTransferType TokenTransferType { get; set; }

            public PublicKey Mint { get; set; }

            public ulong Amount { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WritePubKey(From, offset);
                offset += 32;
                if (To != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WritePubKey(To, offset);
                    offset += 32;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                _data.WriteU8((byte)TokenTransferType, offset);
                offset += 1;
                _data.WritePubKey(Mint, offset);
                offset += 32;
                _data.WriteU64(Amount, offset);
                offset += 8;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TokenDelta result)
            {
                int offset = initialOffset;
                result = new TokenDelta();
                result.From = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.To = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.TokenTransferType = (TokenTransferType)_data.GetU8(offset);
                offset += 1;
                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                return offset - initialOffset;
            }
        }

        public partial class TokenValidation
        {
            public Filter Filter { get; set; }

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
                offset += Filter.Deserialize(_data, offset, out var resultFilter);
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

        public enum MatchState : byte
        {
            Draft,
            Initialized,
            Started,
            Finalized,
            PaidOut,
            Deactivated
        }

        public enum InheritanceState : byte
        {
            NotInherited,
            Inherited,
            Overridden
        }

        public enum TokenTransferType : byte
        {
            PlayerToPlayer,
            PlayerToEntrant,
            Normal
        }

        public enum FilterType : byte
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

        public partial class Filter
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
                    case FilterType.Namespace:
                        offset += NamespaceValue.Serialize(_data, offset);
                        break;
                    case FilterType.Parent:
                        offset += ParentValue.Serialize(_data, offset);
                        break;
                    case FilterType.Mint:
                        offset += MintValue.Serialize(_data, offset);
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

                    case FilterType.Parent:
                    {
                        ParentType tmpParentValue = new ParentType();
                        offset += ParentType.Deserialize(_data, offset, out tmpParentValue);
                        result.ParentValue = tmpParentValue;
                        break;
                    }

                    case FilterType.Mint:
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

    public partial class RaindropsMatchesClient : TransactionalBaseClient<RaindropsMatchesErrorKind>
    {
        public RaindropsMatchesClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Match>>> GetMatchsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = Match.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Match>>(res);
            List<Match> resultingAccounts = new List<Match>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Match.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<Match>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerWinCallbackBitmap>>> GetPlayerWinCallbackBitmapsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = PlayerWinCallbackBitmap.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerWinCallbackBitmap>>(res);
            List<PlayerWinCallbackBitmap> resultingAccounts = new List<PlayerWinCallbackBitmap>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PlayerWinCallbackBitmap.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<PlayerWinCallbackBitmap>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<WinOracle>>> GetWinOraclesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = WinOracle.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<WinOracle>>(res);
            List<WinOracle> resultingAccounts = new List<WinOracle>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => WinOracle.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<WinOracle>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<Match>> GetMatchAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<Match>(res);
            var resultingAccount = Match.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<Match>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<PlayerWinCallbackBitmap>> GetPlayerWinCallbackBitmapAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerWinCallbackBitmap>(res);
            var resultingAccount = PlayerWinCallbackBitmap.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<PlayerWinCallbackBitmap>(res, resultingAccount);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<WinOracle>> GetWinOracleAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<WinOracle>(res);
            var resultingAccount = WinOracle.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<WinOracle>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeMatchAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, Match> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Match parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Match.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribePlayerWinCallbackBitmapAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, PlayerWinCallbackBitmap> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                PlayerWinCallbackBitmap parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = PlayerWinCallbackBitmap.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeWinOracleAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, WinOracle> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                WinOracle parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = WinOracle.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendCreateOrUpdateOracleAsync(CreateOrUpdateOracleAccounts accounts, CreateOrUpdateOracleArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.CreateOrUpdateOracle(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCreateMatchAsync(CreateMatchAccounts accounts, CreateMatchArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.CreateMatch(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateMatchAsync(UpdateMatchAccounts accounts, UpdateMatchArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.UpdateMatch(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateMatchFromOracleAsync(UpdateMatchFromOracleAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.UpdateMatchFromOracle(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainOracleAsync(DrainOracleAccounts accounts, DrainOracleArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.DrainOracle(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDrainMatchAsync(DrainMatchAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.DrainMatch(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendLeaveMatchAsync(LeaveMatchAccounts accounts, LeaveMatchArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.LeaveMatch(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDisburseTokensByOracleAsync(DisburseTokensByOracleAccounts accounts, DisburseTokensByOracleArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.DisburseTokensByOracle(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendJoinMatchAsync(JoinMatchAccounts accounts, JoinMatchArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.JoinMatch(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMatchJoinNamespaceAsync(MatchJoinNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.MatchJoinNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMatchLeaveNamespaceAsync(MatchLeaveNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.MatchLeaveNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMatchCacheNamespaceAsync(MatchCacheNamespaceAccounts accounts, ulong page, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.MatchCacheNamespace(accounts, page, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMatchUncacheNamespaceAsync(MatchUncacheNamespaceAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsMatchesProgram.MatchUncacheNamespace(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<RaindropsMatchesErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<RaindropsMatchesErrorKind>>{{6000U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.IncorrectOwner, "Account does not have correct owner!")}, {6001U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.Uninitialized, "Account is not initialized!")}, {6002U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.MintMismatch, "Mint Mismatch!")}, {6003U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.TokenTransferFailed, "Token transfer failed")}, {6004U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NumericalOverflowError, "Numerical overflow error")}, {6005U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.TokenMintToFailed, "Token mint to failed")}, {6006U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.TokenBurnFailed, "TokenBurnFailed")}, {6007U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.DerivedKeyInvalid, "Derived key is invalid")}, {6008U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.InvalidStartingMatchState, "A match can only start in draft or initialized state")}, {6009U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.InvalidUpdateMatchState, "Match authority can only shift from Draft to Initialized or from Initialized/Started to Deactivated")}, {6010U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.InvalidOracleUpdate, "Cannot rely on an oracle until the match has been initialized or started")}, {6011U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.CannotDrainYet, "Cannot drain a match until it is in paid out or deactivated and all token accounts have been drained")}, {6012U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.CannotLeaveMatch, "You can only leave deactivated or paid out matches, or initialized matches with leave_allowed on.")}, {6013U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.ReceiverMustBeSigner, "You must be the person who joined the match to leave it in initialized or started state.")}, {6014U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.PublicKeyMismatch, "Public key mismatch")}, {6015U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.AtaShouldNotHaveDelegate, "To use an ata in this contract, please remove its delegate first")}, {6016U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.CannotEnterMatch, "Can only enter matches in started or initialized state")}, {6017U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.InvalidProof, "Invalid proof")}, {6018U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.RootNotPresent, "Root not present on object")}, {6019U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.MustPassUpObject, "If using roots, must pass up the object you are proving is a member")}, {6020U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NoValidValidationFound, "No valid validations found")}, {6021U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.Blacklisted, "Blacklisted")}, {6022U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NoTokensAllowed, "Tokens are explicitly not allowed in this match")}, {6023U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.InvalidValidation, "This validation will not let in this token")}, {6024U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NoDeltasFound, "This oracle lacks any deltas")}, {6025U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.UsePlayerEndpoint, "Please use the player-specific endpoint for token transfers from a player")}, {6026U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.FromDoesNotMatch, "The original_sender argument does not match the from on the token delta")}, {6027U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.CannotDeltaMoreThanAmountPresent, "Cannot give away more than is present in the token account")}, {6028U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.DeltaMintDoesNotMatch, "Delta mint must match provided token mint account")}, {6029U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.DestinationMismatch, "The given destination token account does not match the delta to field")}, {6030U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.MatchMustBeInFinalized, "Match must be in finalized state to diburse")}, {6031U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.AtaDelegateMismatch, "ATA delegate mismatch")}, {6032U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.OracleAlreadyFinalized, "Oracle already finalized")}, {6033U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.OracleCooldownNotPassed, "Oracle cooldown not over")}, {6034U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.MatchMustBeDrained, "Match must be drained first")}, {6035U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NoParentPresent, "No parent present")}, {6036U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.ReinitializationDetected, "Reinitialization hack detected")}, {6037U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.FailedToLeaveNamespace, "Failed to leave Namespace")}, {6038U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.FailedToJoinNamespace, "Failed to join Namespace")}, {6039U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.UnauthorizedCaller, "Unauthorized Caller")}, {6040U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.FailedToCache, "Failed to cache")}, {6041U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.FailedToUncache, "Failed to uncache")}, {6042U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.AlreadyCached, "Already cached")}, {6043U, new ProgramError<RaindropsMatchesErrorKind>(RaindropsMatchesErrorKind.NotCached, "Not cached")}, };
        }
    }

    namespace Program
    {
        public class CreateOrUpdateOracleAccounts
        {
            public PublicKey Oracle { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class CreateMatchAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class UpdateMatchAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey WinOracle { get; set; }

            public PublicKey Authority { get; set; }
        }

        public class UpdateMatchFromOracleAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey WinOracle { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class DrainOracleAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Oracle { get; set; }

            public PublicKey Authority { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class DrainMatchAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Authority { get; set; }

            public PublicKey Receiver { get; set; }
        }

        public class LeaveMatchAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Receiver { get; set; }

            public PublicKey TokenAccountEscrow { get; set; }

            public PublicKey TokenMint { get; set; }

            public PublicKey DestinationTokenAccount { get; set; }

            public PublicKey TokenProgram { get; set; }
        }

        public class DisburseTokensByOracleAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey TokenAccountEscrow { get; set; }

            public PublicKey TokenMint { get; set; }

            public PublicKey DestinationTokenAccount { get; set; }

            public PublicKey WinOracle { get; set; }

            public PublicKey OriginalSender { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class JoinMatchAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey TokenTransferAuthority { get; set; }

            public PublicKey TokenAccountEscrow { get; set; }

            public PublicKey TokenMint { get; set; }

            public PublicKey SourceTokenAccount { get; set; }

            public PublicKey SourceItemOrPlayerPda { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey ValidationProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }
        }

        public class MatchJoinNamespaceAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class MatchLeaveNamespaceAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class MatchCacheNamespaceAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public class MatchUncacheNamespaceAccounts
        {
            public PublicKey MatchInstance { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Instructions { get; set; }
        }

        public static class RaindropsMatchesProgram
        {
            public static Solana.Unity.Rpc.Models.TransactionInstruction CreateOrUpdateOracle(CreateOrUpdateOracleAccounts accounts, CreateOrUpdateOracleArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Oracle, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6040350293299921555UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction CreateMatch(CreateMatchAccounts accounts, CreateMatchArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11894444524605801067UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateMatch(UpdateMatchAccounts accounts, UpdateMatchArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.WinOracle, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11646978190510912140UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction UpdateMatchFromOracle(UpdateMatchFromOracleAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.WinOracle, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(12556274171806792484UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainOracle(DrainOracleAccounts accounts, DrainOracleArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Oracle, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Receiver, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(7200160660196510069UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DrainMatch(DrainMatchAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(7018562148826581251UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction LeaveMatch(LeaveMatchAccounts accounts, LeaveMatchArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Receiver, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenAccountEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.DestinationTokenAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1127693658511904455UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction DisburseTokensByOracle(DisburseTokensByOracleAccounts accounts, DisburseTokensByOracleArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenAccountEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.DestinationTokenAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.WinOracle, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.OriginalSender, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1185569732726140263UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction JoinMatch(JoinMatchAccounts accounts, JoinMatchArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenTransferAuthority, true), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenAccountEscrow, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.TokenMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.SourceTokenAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SourceItemOrPlayerPda, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ValidationProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(3220983856503916788UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction MatchJoinNamespace(MatchJoinNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14392709822521900844UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction MatchLeaveNamespace(MatchLeaveNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11283990399198847868UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction MatchCacheNamespace(MatchCacheNamespaceAccounts accounts, ulong page, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(12424812423772493373UL, offset);
                offset += 8;
                _data.WriteU64(page, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction MatchUncacheNamespace(MatchUncacheNamespaceAccounts accounts, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.MatchInstance, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Instructions, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13627689026227051403UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }
}