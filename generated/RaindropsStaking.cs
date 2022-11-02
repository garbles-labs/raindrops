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
using RaindropsStaking;
using RaindropsStaking.Program;
using RaindropsStaking.Errors;
using RaindropsStaking.Accounts;
using RaindropsStaking.Types;

namespace RaindropsStaking
{
    namespace Accounts
    {
        public partial class StakingCounter
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 2386424562939491517UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[]{189, 116, 70, 5, 127, 72, 30, 33};
            public static string ACCOUNT_DISCRIMINATOR_B58 => "YgwhEuYqTnQ";
            public byte Bump { get; set; }

            public long EventStart { get; set; }

            public byte EventType { get; set; }

            public static StakingCounter Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                StakingCounter result = new StakingCounter();
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.EventStart = _data.GetS64(offset);
                offset += 8;
                result.EventType = _data.GetU8(offset);
                offset += 1;
                return result;
            }
        }
    }

    namespace Errors
    {
        public enum RaindropsStakingErrorKind : uint
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
            InvalidMintAuthority = 6015U,
            NotMintAuthority = 6016U,
            MustBeHolderToBuild = 6017U,
            MissingMerkleInfo = 6018U,
            InvalidProof = 6019U,
            UnableToFindValidCooldownState = 6020U,
            StakingWarmupNotStarted = 6021U,
            StakingWarmupNotFinished = 6022U,
            IncorrectStakingCounterType = 6023U,
            StakingCooldownNotStarted = 6024U,
            StakingCooldownNotFinished = 6025U,
            InvalidProgramOwner = 6026U,
            NotInitialized = 6027U,
            StakingMintNotWhitelisted = 6028U,
            DiscriminatorMismatch = 6029U,
            StakingForPlayerComingSoon = 6030U
        }
    }

    namespace Types
    {
        public partial class BeginArtifactStakeWarmupArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong Index { get; set; }

            public ulong StakingIndex { get; set; }

            public PublicKey ArtifactClassMint { get; set; }

            public PublicKey ArtifactMint { get; set; }

            public ulong StakingAmount { get; set; }

            public PermissivenessType StakingPermissivenessToUse { get; set; }

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

                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(StakingIndex, offset);
                offset += 8;
                _data.WritePubKey(ArtifactClassMint, offset);
                offset += 32;
                _data.WritePubKey(ArtifactMint, offset);
                offset += 32;
                _data.WriteU64(StakingAmount, offset);
                offset += 8;
                if (StakingPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)StakingPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BeginArtifactStakeWarmupArgs result)
            {
                int offset = initialOffset;
                result = new BeginArtifactStakeWarmupArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.Index = _data.GetU64(offset);
                offset += 8;
                result.StakingIndex = _data.GetU64(offset);
                offset += 8;
                result.ArtifactClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ArtifactMint = _data.GetPubKey(offset);
                offset += 32;
                result.StakingAmount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.StakingPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class EndArtifactStakeWarmupArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public ulong StakingIndex { get; set; }

            public PublicKey ArtifactClassMint { get; set; }

            public PublicKey ArtifactMint { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(StakingIndex, offset);
                offset += 8;
                _data.WritePubKey(ArtifactClassMint, offset);
                offset += 32;
                _data.WritePubKey(ArtifactMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EndArtifactStakeWarmupArgs result)
            {
                int offset = initialOffset;
                result = new EndArtifactStakeWarmupArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.StakingIndex = _data.GetU64(offset);
                offset += 8;
                result.ArtifactClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ArtifactMint = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public partial class BeginArtifactStakeCooldownArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong? ParentClassIndex { get; set; }

            public ulong Index { get; set; }

            public ulong StakingIndex { get; set; }

            public PublicKey ArtifactClassMint { get; set; }

            public PublicKey ArtifactMint { get; set; }

            public PermissivenessType StakingPermissivenessToUse { get; set; }

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

                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(StakingIndex, offset);
                offset += 8;
                _data.WritePubKey(ArtifactClassMint, offset);
                offset += 32;
                _data.WritePubKey(ArtifactMint, offset);
                offset += 32;
                if (StakingPermissivenessToUse != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU8((byte)StakingPermissivenessToUse, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BeginArtifactStakeCooldownArgs result)
            {
                int offset = initialOffset;
                result = new BeginArtifactStakeCooldownArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.ParentClassIndex = _data.GetU64(offset);
                    offset += 8;
                }

                result.Index = _data.GetU64(offset);
                offset += 8;
                result.StakingIndex = _data.GetU64(offset);
                offset += 8;
                result.ArtifactClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ArtifactMint = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.StakingPermissivenessToUse = (PermissivenessType)_data.GetU8(offset);
                    offset += 1;
                }

                return offset - initialOffset;
            }
        }

        public partial class EndArtifactStakeCooldownArgs
        {
            public ulong ClassIndex { get; set; }

            public ulong Index { get; set; }

            public ulong StakingIndex { get; set; }

            public PublicKey ArtifactClassMint { get; set; }

            public PublicKey ArtifactMint { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU64(ClassIndex, offset);
                offset += 8;
                _data.WriteU64(Index, offset);
                offset += 8;
                _data.WriteU64(StakingIndex, offset);
                offset += 8;
                _data.WritePubKey(ArtifactClassMint, offset);
                offset += 32;
                _data.WritePubKey(ArtifactMint, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EndArtifactStakeCooldownArgs result)
            {
                int offset = initialOffset;
                result = new EndArtifactStakeCooldownArgs();
                result.ClassIndex = _data.GetU64(offset);
                offset += 8;
                result.Index = _data.GetU64(offset);
                offset += 8;
                result.StakingIndex = _data.GetU64(offset);
                offset += 8;
                result.ArtifactClassMint = _data.GetPubKey(offset);
                offset += 32;
                result.ArtifactMint = _data.GetPubKey(offset);
                offset += 32;
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
    }

    public partial class RaindropsStakingClient : TransactionalBaseClient<RaindropsStakingErrorKind>
    {
        public RaindropsStakingClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<StakingCounter>>> GetStakingCountersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solana.Unity.Rpc.Models.MemCmp>{new Solana.Unity.Rpc.Models.MemCmp{Bytes = StakingCounter.ACCOUNT_DISCRIMINATOR_B58, Offset = 0}};
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<StakingCounter>>(res);
            List<StakingCounter> resultingAccounts = new List<StakingCounter>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => StakingCounter.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solana.Unity.Programs.Models.ProgramAccountsResultWrapper<List<StakingCounter>>(res, resultingAccounts);
        }

        public async Task<Solana.Unity.Programs.Models.AccountResultWrapper<StakingCounter>> GetStakingCounterAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solana.Unity.Programs.Models.AccountResultWrapper<StakingCounter>(res);
            var resultingAccount = StakingCounter.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solana.Unity.Programs.Models.AccountResultWrapper<StakingCounter>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeStakingCounterAsync(string accountAddress, Action<SubscriptionState, Solana.Unity.Rpc.Messages.ResponseValue<Solana.Unity.Rpc.Models.AccountInfo>, StakingCounter> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                StakingCounter parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = StakingCounter.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendBeginArtifactStakeWarmupAsync(BeginArtifactStakeWarmupAccounts accounts, BeginArtifactStakeWarmupArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsStakingProgram.BeginArtifactStakeWarmup(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendEndArtifactStakeWarmupAsync(EndArtifactStakeWarmupAccounts accounts, EndArtifactStakeWarmupArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsStakingProgram.EndArtifactStakeWarmup(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBeginArtifactStakeCooldownAsync(BeginArtifactStakeCooldownAccounts accounts, BeginArtifactStakeCooldownArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsStakingProgram.BeginArtifactStakeCooldown(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendEndArtifactStakeCooldownAsync(EndArtifactStakeCooldownAccounts accounts, EndArtifactStakeCooldownArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solana.Unity.Rpc.Models.TransactionInstruction instr = Program.RaindropsStakingProgram.EndArtifactStakeCooldown(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<RaindropsStakingErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<RaindropsStakingErrorKind>>{{6000U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.IncorrectOwner, "Account does not have correct owner!")}, {6001U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.Uninitialized, "Account is not initialized!")}, {6002U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.MintMismatch, "Mint Mismatch!")}, {6003U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.TokenTransferFailed, "Token transfer failed")}, {6004U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.NumericalOverflowError, "Numerical overflow error")}, {6005U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.TokenMintToFailed, "Token mint to failed")}, {6006U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.TokenBurnFailed, "TokenBurnFailed")}, {6007U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.DerivedKeyInvalid, "Derived key is invalid")}, {6008U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.MustSpecifyPermissivenessType, "Update authority for metadata expected as signer")}, {6009U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.PermissivenessNotFound, "Permissiveness not found in array")}, {6010U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.PublicKeyMismatch, "Public key mismatch")}, {6011U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.InsufficientBalance, "Insufficient Balance")}, {6012U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.MetadataDoesntExist, "Metadata doesn't exist")}, {6013U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.EditionDoesntExist, "Edition doesn't exist")}, {6014U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.NoParentPresent, "No parent present")}, {6015U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.InvalidMintAuthority, "Invalid mint authority")}, {6016U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.NotMintAuthority, "Not mint authority")}, {6017U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.MustBeHolderToBuild, "Must be token holder to build against it")}, {6018U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.MissingMerkleInfo, "Missing the merkle fields")}, {6019U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.InvalidProof, "Invalid proof")}, {6020U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.UnableToFindValidCooldownState, "Unable to find a valid cooldown state")}, {6021U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingWarmupNotStarted, "You havent started staking yet")}, {6022U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingWarmupNotFinished, "You havent finished your warm up period")}, {6023U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.IncorrectStakingCounterType, "Attempting to use a staking counter going in the wrong direction")}, {6024U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingCooldownNotStarted, "Staking cooldown not started")}, {6025U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingCooldownNotFinished, "Staking cooldown not finished")}, {6026U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.InvalidProgramOwner, "Invalid program owner")}, {6027U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.NotInitialized, "Not initialized")}, {6028U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingMintNotWhitelisted, "Staking mint not whitelisted")}, {6029U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.DiscriminatorMismatch, "Discriminator mismatch")}, {6030U, new ProgramError<RaindropsStakingErrorKind>(RaindropsStakingErrorKind.StakingForPlayerComingSoon, "Staking for player coming soon")}, };
        }
    }

    namespace Program
    {
        public class BeginArtifactStakeWarmupAccounts
        {
            public PublicKey ArtifactClass { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey ArtifactIntermediaryStakingAccount { get; set; }

            public PublicKey ArtifactIntermediaryStakingCounter { get; set; }

            public PublicKey StakingAccount { get; set; }

            public PublicKey StakingMint { get; set; }

            public PublicKey StakingTransferAuthority { get; set; }

            public PublicKey Namespace { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Clock { get; set; }
        }

        public class EndArtifactStakeWarmupAccounts
        {
            public PublicKey ArtifactClass { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey ArtifactIntermediaryStakingAccount { get; set; }

            public PublicKey ArtifactIntermediaryStakingCounter { get; set; }

            public PublicKey ArtifactMintStakingAccount { get; set; }

            public PublicKey StakingMint { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey ItemProgram { get; set; }

            public PublicKey PlayerProgram { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Clock { get; set; }

            public PublicKey InstructionSysvarAccount { get; set; }
        }

        public class BeginArtifactStakeCooldownAccounts
        {
            public PublicKey ArtifactClass { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey ArtifactIntermediaryStakingAccount { get; set; }

            public PublicKey ArtifactIntermediaryStakingCounter { get; set; }

            public PublicKey ArtifactMintStakingAccount { get; set; }

            public PublicKey StakingAccount { get; set; }

            public PublicKey StakingMint { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey ItemProgram { get; set; }

            public PublicKey PlayerProgram { get; set; }

            public PublicKey SystemProgram { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Rent { get; set; }

            public PublicKey Clock { get; set; }

            public PublicKey InstructionSysvarAccount { get; set; }
        }

        public class EndArtifactStakeCooldownAccounts
        {
            public PublicKey ArtifactClass { get; set; }

            public PublicKey Artifact { get; set; }

            public PublicKey ArtifactIntermediaryStakingAccount { get; set; }

            public PublicKey ArtifactIntermediaryStakingCounter { get; set; }

            public PublicKey StakingAccount { get; set; }

            public PublicKey StakingMint { get; set; }

            public PublicKey Payer { get; set; }

            public PublicKey TokenProgram { get; set; }

            public PublicKey Clock { get; set; }
        }

        public static class RaindropsStakingProgram
        {
            public static Solana.Unity.Rpc.Models.TransactionInstruction BeginArtifactStakeWarmup(BeginArtifactStakeWarmupAccounts accounts, BeginArtifactStakeWarmupArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ArtifactClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingCounter, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingTransferAuthority, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Namespace, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6437419876754629370UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction EndArtifactStakeWarmup(EndArtifactStakeWarmupAccounts accounts, EndArtifactStakeWarmupArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ArtifactClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingCounter, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactMintStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.InstructionSysvarAccount, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6486869591294122074UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction BeginArtifactStakeCooldown(BeginArtifactStakeCooldownAccounts accounts, BeginArtifactStakeCooldownArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ArtifactClass, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingCounter, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactMintStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.StakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingMint, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ItemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.PlayerProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.InstructionSysvarAccount, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(3408900588870897778UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solana.Unity.Rpc.Models.TransactionInstruction EndArtifactStakeCooldown(EndArtifactStakeCooldownAccounts accounts, EndArtifactStakeCooldownArgs args, PublicKey programId)
            {
                List<Solana.Unity.Rpc.Models.AccountMeta> keys = new()
                {Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.ArtifactClass, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Artifact, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.ArtifactIntermediaryStakingCounter, false), Solana.Unity.Rpc.Models.AccountMeta.Writable(accounts.StakingAccount, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.StakingMint, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solana.Unity.Rpc.Models.AccountMeta.ReadOnly(accounts.Clock, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(11478652310794244186UL, offset);
                offset += 8;
                offset += args.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solana.Unity.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }
}