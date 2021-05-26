using System;
using AppSoftware.LicenceEngine.Common;
using AppSoftware.LicenceEngine.KeyGenerator;
using AppSoftware.LicenceEngine.KeyVerification;

namespace LicensingApi.Logic
{
    public static class KeyHandler
    {
        private static readonly KeyByteSet[] generator_keyByteSets = new[]
            {
                   new KeyByteSet(keyByteNumber: 1, keyByteA: 58, keyByteB: 6, keyByteC: 97),
                   new KeyByteSet(keyByteNumber: 2, keyByteA: 96, keyByteB: 254, keyByteC: 23),
                   new KeyByteSet(keyByteNumber: 3, keyByteA: 11, keyByteB: 185, keyByteC: 69),
                   new KeyByteSet(keyByteNumber: 4, keyByteA: 2, keyByteB: 93, keyByteC: 41),
                   new KeyByteSet(keyByteNumber: 5, keyByteA: 62, keyByteB: 4, keyByteC: 234),
                   new KeyByteSet(keyByteNumber: 6, keyByteA: 200, keyByteB: 56, keyByteC: 49),
                   new KeyByteSet(keyByteNumber: 7, keyByteA: 89, keyByteB: 45, keyByteC: 142),
                   new KeyByteSet(keyByteNumber: 8, keyByteA: 6, keyByteB: 88, keyByteC: 32)
            };
        private static readonly KeyByteSet[] validator_keyByteSets = new[]
            {
                    new KeyByteSet(keyByteNumber: 1, keyByteA: 58, keyByteB: 6, keyByteC: 97),
                    new KeyByteSet(keyByteNumber: 5, keyByteA: 62, keyByteB: 4, keyByteC: 234),
                    new KeyByteSet(keyByteNumber: 8, keyByteA: 6, keyByteB: 88, keyByteC: 32)
            };
        public static string GenerateKey()
        {
            int seed = new Random().Next(0, int.MaxValue); // generated random seed. Seed can be user id / number of users / etc.
            var pkvKeyGenerator = new PkvKeyGenerator();
            string licenceKey = pkvKeyGenerator.MakeKey(seed, generator_keyByteSets);

            return licenceKey;
        }

        public static bool ValidateKey(string licenseKey)
        {
            var pkvKeyVerifier = new PkvKeyVerifier();

            PkvKeyVerificationResult pkvKeyVerificationResult = pkvKeyVerifier.VerifyKey(

                key: licenseKey?.Trim(),
                keyByteSetsToVerify: validator_keyByteSets,
                totalKeyByteSets: 8,
                blackListedSeeds: null
            );

            if (pkvKeyVerificationResult == PkvKeyVerificationResult.KeyIsValid)
            {
                return true;
            }
            return false;

        }
    }
}