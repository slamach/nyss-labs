using CryptoApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoApp.Tests.Models
{
    [TestClass]
    public class VigenereCryptographerTest
    {
        [TestMethod]
        public void EncryptDefaultAlphabetIsRight()
        {
            VigenereCryptographer vigenereCryptographer = new VigenereCryptographer();

            string result1 = vigenereCryptographer.Encrypt("ура тесты! test, и еще один тест", "тест");
            string result2 = vigenereCryptographer.Encrypt("в принципе понять", "скорпион");
            string result3 = vigenereCryptographer.Encrypt("тестовая строкаfosdpf!!", "test");

            Assert.AreEqual("ёхс ечцдн! test, ы йкч биъа ейге", result1);
            Assert.AreEqual("у ъящэячэц ъэюоык", result2);
            Assert.AreEqual("тестовая строкаfosdpf!!", result3);
        }

        [TestMethod]
        public void DecryptDefaultAlphabetIsRight()
        {
            VigenereCryptographer vigenereCryptographer = new VigenereCryptographer();

            string result1 = vigenereCryptographer.Decrypt("т чее ё тсд чюц бцтс вбфмебььт, yes, master;;", "тест");
            string result2 = vigenereCryptographer.Decrypt("бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!! ", "скорпион");
            string result3 = vigenereCryptographer.Decrypt("тестовая строкаfosdpf!!", "test");

            Assert.AreEqual("а тут у нас еще одна попыточка, yes, master;;", result1);
            Assert.AreEqual("поздравляю, ты получил исходный текст!!! ", result2);
            Assert.AreEqual("тестовая строкаfosdpf!!", result3);
        }

        [TestMethod]
        public void EncryptEnglishAlphabetIsRight()
        {
            VigenereCryptographer vigenereCryptographer = new VigenereCryptographer("abcdefghijklmnopqrstuvwxyz");
            string result2 = vigenereCryptographer.Encrypt("basically understand", "scorpion");
            string result3 = vigenereCryptographer.Encrypt("тестовая строкаfosdpf!!", "тест");

            string result = vigenereCryptographer.Encrypt("o my god roy это тесты!!! :) no", "goodbye");

            Assert.AreEqual("u am jpb vum это тесты!!! :) br", result);
            Assert.AreEqual("tcgzrizyq wbutzggspr", result2);
            Assert.AreEqual("тестовая строкаfosdpf!!", result3);
        }

        [TestMethod]
        public void DecryptEnglishAlphabetIsRight()
        {
            VigenereCryptographer vigenereCryptographer = new VigenereCryptographer("abcdefghijklmnopqrstuvwxyz");

            string result = vigenereCryptographer.Decrypt("zsgw pl qe hsvu umzv ab ucwz это тест 093___12)", "goodbye");
            string result2 = vigenereCryptographer.Decrypt("uqbxgihhdchzdvg lgw ufi bvr gtwxxvoy lglk!!! ", "scorpion");
            string result3 = vigenereCryptographer.Decrypt("тестовая строкаfosdpf!!", "тест");

            Assert.AreEqual("test on my test with my test это тест 093___12)", result);
            Assert.AreEqual("congratulations you got the original text!!! ", result2);
            Assert.AreEqual("тестовая строкаfosdpf!!", result3);
        }
    }
}
