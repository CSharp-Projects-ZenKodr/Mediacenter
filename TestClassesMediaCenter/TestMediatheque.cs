using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassesMediaCenter;

namespace TestClassesMediaCenter
{
    /// <summary>
    /// Description résumée pour TestMediatheque
    /// </summary>
    [TestClass]
    public class TestMediatheque
    {
        public TestMediatheque()
        {
            
        }

        /// <summary>
        /// Test de la construction de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestConstructMediathequeMediasApercus()
        {
            Mediatheque mediatheque = new Mediatheque("/dossierMedia", "/dossierApercus");
            String resMedias = "/dossierMedia";
            String resApercus = "/dossierApercus";
            Assert.AreEqual(mediatheque.Medias, resMedias);
            Assert.AreEqual(mediatheque.Apercus, resApercus);
        }

        /// <summary>
        /// Test de la construction de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestConstructMediathequeMedia()
        {
            Mediatheque mediatheque = new Mediatheque("/dossierMedia");
            String resMedias = "/dossierMedia";
            Assert.AreEqual(mediatheque.Medias, resMedias);
        }

        /// <summary>
        /// Test de la fonction AddMedia() de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestAddMedia()
        {
            Mediatheque mediatheque = new Mediatheque(@"C:");
            mediatheque.AddMedia();
            Assert.AreNotEqual(0, mediatheque.getMedias().Length);
        }

        /// <summary>
        /// Test de la fonction AddMedia(Media media) de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestAddMediaExistant()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            mediatheque.AddMedia(media);

            Assert.AreEqual(media, mediatheque.getMedias()[0]);
        }

        /// <summary>
        /// Test de la fonction AddMedia(Media media) de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            mediatheque.AddMedia(media);
            mediatheque.Remove(media);

            Assert.AreEqual(0, mediatheque.getMedias().Length);
        }

        /// <summary>
        /// Test de la fonction AddMedia(Media media) de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestGetMedias()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            mediatheque.AddMedia(media);

            Assert.AreSame(media, mediatheque.getMedias()[0]);
        }

        /// <summary>
        /// Test de la fonction AddMedia(Media media) de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestGetMediasByAuthor()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            media.Auteur = "George Lucas";
            mediatheque.AddMedia(media);

            Assert.AreSame(media, mediatheque.getMediasByAuthor(media.Auteur)[0]);
        }

        /// <summary>
        /// Test de la fonction AddMedia(Media media) de la classe Mediatheque
        /// </summary>
        [TestMethod]
        public void TestGetMediasByTitle()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            mediatheque.AddMedia(media);

            Assert.AreSame(media, mediatheque.getMediasByTitle(media.Titre)[0]);
        }

        /// <summary>
        /// Test permettant de tester la fonction getMediasByYear(int annee) de
        /// la classe Mediatheque.
        /// </summary>
        [TestMethod]
        public void TestGetMediasAnnee()
        {
            Mediatheque mediatheque = new Mediatheque(@"D:\Film");
            Media media = new Media(@"c:\temp\star wars.mp4", false, true, 150);
            media.Annee = 1977;

            mediatheque.AddMedia(media);

            Assert.AreSame(media, mediatheque.getMediasByYear(1977)[0]);
        }
    }
}
