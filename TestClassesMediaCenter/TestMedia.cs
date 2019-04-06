using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassesMediaCenter;
namespace TestClassesMediaCenter
{
    [TestClass]
    public class TestMedia
    {
        /// <summary>
        /// Test de la création d'un média
        /// </summary>
        [TestMethod]
        public void TestConstructMedia()
        {
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            String resName = "Star wars";
            bool resAudio = false;
            bool resVideo = true;
            int resDuree = 150;

            Assert.AreEqual(media.Titre, resName);
            Assert.AreEqual(media.Audio, resAudio);
            Assert.AreEqual(media.Video, resVideo);
            Assert.AreEqual(media.Duree, resDuree);
        }

        /// <summary>
        /// Test de la fonction ToString() de la classe Media
        /// </summary>
        [TestMethod]
        public void TestMediaToString()
        {
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            Assert.AreEqual(media.ToString(), "Star wars (, 0)"); //L'année et l'auteur ne sont pas encore disponible.
        }
    }
}
