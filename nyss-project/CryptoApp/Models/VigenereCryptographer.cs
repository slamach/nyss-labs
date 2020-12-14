using System;

namespace CryptoApp.Models
{
    public class VigenereCryptographer
    {
        private const string DefaultAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        readonly string alphabet;

        public VigenereCryptographer(string alphabet = null)
        {
            this.alphabet = String.IsNullOrEmpty(alphabet) ? DefaultAlphabet : alphabet;
        }

        private string Crypt(string input, string key, bool encrypting)
        {
            string result = "";
            int keyPosition = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int letterIndex = alphabet.IndexOf(Char.ToLower(input[i]));
                if (letterIndex >= 0)
                {
                    int keyIndex = alphabet.IndexOf(key[keyPosition]);
                    if (keyIndex == -1)
                    {
                        return input;
                    }

                    char newLetter = encrypting ?
                        alphabet[(alphabet.Length + letterIndex + keyIndex) % alphabet.Length] :
                        alphabet[(alphabet.Length + letterIndex - keyIndex) % alphabet.Length];
                    result += Char.IsUpper(input[i]) ? Char.ToUpper(newLetter) : newLetter;
                    keyPosition = keyPosition + 1 >= key.Length ? 0 : keyPosition + 1;
                }
                else
                {
                    result += input[i].ToString();
                }
            }
            return result;
        }

        public string Encrypt(string input, string key)
        {
            return Crypt(input, key, true);
        }

        public string Decrypt(string input, string key)
        {
            return Crypt(input, key, false);
        }
    }
}