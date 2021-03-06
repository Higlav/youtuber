﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Youtuber.Net;

namespace youtubertest
{
    [TestClass]
    public class URLUtilityTest
    {
        private const string VIDEOID = "TWcyIpul8OE";
        private const string PLAYLISTID = "RDd2Y4dFVgS8g";
        private const string PLAYLISTID2 = "PLDfKAXSi6kUYmFmt-2_TIwHkYIEq2HyDD";
        //https://www.youtube.com/watch?v=iVTqxdEXFkA&list=PLDfKAXSi6kUYmFmt-2_TIwHkYIEq2HyDD
        //https://www.youtube.com/playlist?list=PLDfKAXSi6kUYmFmt-2_TIwHkYIEq2HyDD

        [TestMethod]
        public void ValidateVideoURLs(){
            URLResult validVideo = URLResult.IsValid |
                                   URLResult.HasVideoID |
                                   URLResult.IsVideo;

            foreach (string url in new[]{
                "https://www.youtube.com/watch?v=" + VIDEOID,
                "https://youtube.com/watch?v=" + VIDEOID,
                "https://www.m.youtube.com/watch?v=" + VIDEOID,
                "https://m.youtube.com/watch?v=" + VIDEOID,
                "https://www.youtu.be/" + VIDEOID,
                "https://youtu.be/" + VIDEOID,
                "https://www.youtube-nocookie.com/embed/" + VIDEOID,
                "https://youtube-nocookie.com/embed/" + VIDEOID
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(validVideo, URLUtility.AnalyzeURI(uri));
            }
        }

        [TestMethod]
        public void ValidatePlaylistUrls(){
            URLResult validVideoOfPlaylist = URLResult.IsValid |
                                             URLResult.HasVideoID |
                                             URLResult.IsVideo |
                                             URLResult.IsPlaylist;

            foreach (string url in new[]{
                $"https://www.youtube.com/watch?v={VIDEOID}&list={PLAYLISTID}",
                $"https://www.youtube.com:443/watch?v={VIDEOID}&list={PLAYLISTID}",
                $"https://youtube.com/watch?v={VIDEOID}&list={PLAYLISTID}",
                $"https://www.m.youtube.com/watch?v={VIDEOID}&list={PLAYLISTID}",
                $"https://m.youtube.com/watch?v={VIDEOID}&list={PLAYLISTID}"
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(validVideoOfPlaylist, URLUtility.AnalyzeURI(uri));
            }

            URLResult validPlaylist = URLResult.IsValid | URLResult.IsPlaylist;
            foreach (string url in new[]{
                $"https://www.youtube.com/playlist?list={PLAYLISTID2}",
                $"https://youtube.com/playlist?list={PLAYLISTID2}"
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(validPlaylist, URLUtility.AnalyzeURI(uri));
            }
        }

        [TestMethod]
        public void ValidateImageUrls(){
            URLResult validImage = URLResult.IsValid |
                                   URLResult.HasVideoID |
                                   URLResult.IsImage;

            foreach (string url in new[]{
                $"https://img.youtube.com/vi/{VIDEOID}/0.jpg",
                $"https://i.ytimg.com/vi/{VIDEOID}/1.jpg",
                $"https://i1.ytimg.com/vi/{VIDEOID}/2.jpg",
                $"https://i2.ytimg.com/vi/{VIDEOID}/3.jpg",
                $"https://i3.ytimg.com/vi/{VIDEOID}/default.jpg",
                $"https://i4.ytimg.com/vi/{VIDEOID}/hqdefault.jpg",
                $"https://i4.ytimg.com/vi/{VIDEOID}/mqdefault.jpg",
                $"https://i4.ytimg.com/vi/{VIDEOID}/sddefault.jpg",
                $"https://i4.ytimg.com/vi/{VIDEOID}/maxresdefault.jpg"
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(validImage, URLUtility.AnalyzeURI(uri));
            }
        }

        [TestMethod]
        public void InvalidateUrls(){
            foreach (string url in new[]{
                "//google.com/",
                "//youtube.com/",
                "//youtube.com/watch?v=abc",
                "//youtube.com/watch?v==0123456789",
                "http://youtube.com/watch?v=0123456789_",
                "//img.youtube.com/0123456789_/",
                $"https://www.youtu.be/{VIDEOID}&list={PLAYLISTID}",
                $"https://youtu.be/{VIDEOID}&list={PLAYLISTID}",
                $"https://www.youtube-nocookie.com/embed/{VIDEOID}&list={PLAYLISTID}",
                $"https://youtube-nocookie.com/embed/{VIDEOID}&list={PLAYLISTID}"
            }) {
                Uri uri = new Uri(url);
                Assert.IsFalse(URLUtility.AnalyzeURI(uri).HasFlag(URLResult.IsValid));
            }
        }

        [TestMethod]
        public void ExtractVideoID(){
            foreach (string url in new[]{
                "https://www.youtube.com/watch?v=" + VIDEOID,
                "https://youtube.com/watch?v=" + VIDEOID,
                "https://www.m.youtube.com/watch?v=" + VIDEOID,
                "https://m.youtube.com/watch?v=" + VIDEOID,
                "https://www.youtu.be/" + VIDEOID,
                "https://youtu.be/" + VIDEOID,
                "https://www.youtube-nocookie.com/embed/" + VIDEOID,
                "https://youtube-nocookie.com/embed/" + VIDEOID,
                "https://www.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.youtube.com:443/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.m.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://m.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.youtu.be/" + VIDEOID + "&list=" + PLAYLISTID,
                "https://youtu.be/" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.youtube-nocookie.com/embed/" + VIDEOID + "&list=" + PLAYLISTID,
                "https://youtube-nocookie.com/embed/" + VIDEOID + "&list=" + PLAYLISTID,
                "https://img.youtube.com/vi/" + VIDEOID + "/0.jpg",
                "https://i.ytimg.com/vi/" + VIDEOID + "/1.jpg",
                "https://i1.ytimg.com/vi/" + VIDEOID + "/2.jpg",
                "https://i2.ytimg.com/vi/" + VIDEOID + "/3.jpg",
                "https://i3.ytimg.com/vi/" + VIDEOID + "/default.jpg",
                "https://i4.ytimg.com/vi/" + VIDEOID + "/hqdefault.jpg",
                "https://i4.ytimg.com/vi/" + VIDEOID + "/mqdefault.jpg",
                "https://i4.ytimg.com/vi/" + VIDEOID + "/sddefault.jpg",
                "https://i4.ytimg.com/vi/" + VIDEOID + "/maxresdefault.jpg"
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(VIDEOID, URLUtility.ExtractVideoID(uri));
            }
        }

        [TestMethod]
        public void ExtractPlaylistID(){
            foreach (string url in new[]{
                "https://www.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.youtube.com:443/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://www.m.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID,
                "https://m.youtube.com/watch?v=" + VIDEOID + "&list=" + PLAYLISTID
            }) {
                Uri uri = new Uri(url);
                Assert.AreEqual(PLAYLISTID, URLUtility.ExtractPlaylistID(uri));
            }
        }
    }
}