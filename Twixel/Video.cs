﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwixelAPI
{
    /// <summary>
    /// Resolution struct
    /// </summary>
    [Serializable]
    public struct Resolution
    {
        /// <summary>
        /// Width in pixels
        /// </summary>
        public readonly int width;

        /// <summary>
        /// Height in pixels
        /// </summary>
        public readonly int height;

        /// <summary>
        /// Resolution constructor
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }

    /// <summary>
    /// Thumbnail struct
    /// </summary>
    [Serializable]
    public struct Thumbnail
    {
        /// <summary>
        /// Thumbnail url
        /// </summary>
        public readonly Uri url;

        /// <summary>
        /// Thumbnail type
        /// </summary>
        public readonly string type;

        /// <summary>
        /// Thumbnail constructor
        /// </summary>
        /// <param name="url">Thumbnail url</param>
        /// <param name="type">Thumbnail type</param>
        public Thumbnail(string url, string type)
        {
            this.url = new Uri(url);
            this.type = type;
        }
    }

    /// <summary>
    /// Video object
    /// </summary>
    [Serializable]
    public class Video : TwixelObjectBase
    {
        /// <summary>
        /// Title
        /// v2/v3
        /// </summary>
        public string title;

        /// <summary>
        /// Description
        /// v2/v3
        /// </summary>
        public string description;

        /// <summary>
        /// Broadcast ID
        /// v2/v3
        /// </summary>
        public long broadcastId;

        /// <summary>
        /// Status
        /// v2/v3
        /// </summary>
        public string status;

        /// <summary>
        /// Comma seperated string of tags
        /// v3
        /// </summary>
        public string tagList;

        /// <summary>
        /// ID
        /// v2/v3
        /// </summary>
        public string id;

        /// <summary>
        /// Recording date
        /// v2/v3
        /// </summary>
        public DateTime recordedAt;

        /// <summary>
        /// Game, can be null
        /// v2/v3
        /// </summary>
        public string game;

        /// <summary>
        /// Length in seconds
        /// v2/v3
        /// </summary>
        public long length;

        /// <summary>
        /// Link to preview image
        /// v2/v3
        /// </summary>
        public Uri preview;

        /// <summary>
        /// Link to preview images
        /// v5
        /// </summary>
        [System.Runtime.Serialization.OptionalField( VersionAdded = 5 )]
        public Dictionary<string, Uri> previewv5;
        
        /// <summary>
        /// Link to video
        /// v2/v3
        /// </summary>
        public Uri url;

        /// <summary>
        /// Embed string
        /// v2
        /// </summary>
        public string embed;

        /// <summary>
        /// Number of views
        /// v2/v3
        /// </summary>
        public long views;

        /// <summary>
        /// Quality FPS's
        /// Dictionary strings: chunked, high, medium, low, mobile, audio_only
        /// v3
        /// </summary>
        public Dictionary<string, double> fps;

        /// <summary>
        /// Quality resolutions.
        /// Dictionary strings: chunked, high, medium, low, mobile
        /// v3
        /// </summary>
        public Dictionary<string, Resolution> resolutions;

        /// <summary>
        /// Broadcast type
        /// v2/v3
        /// </summary>
        public string broadcastType;

        /// <summary>
        /// Channel info
        /// Dictionary strings: name, display_name
        /// v2/v3
        /// </summary>
        public Dictionary<string, string> channel;

        /// <summary>
        /// Video language
        /// v5
        /// </summary>
        [System.Runtime.Serialization.OptionalField( VersionAdded = 5 )]
        public string language;

        /// <summary>
        /// Viewability settings
        /// v5
        /// </summary>
        [System.Runtime.Serialization.OptionalField( VersionAdded = 5 )]
        public string viewable;

        /// <summary>
        /// Thumbnails
        /// </summary>
        [System.Runtime.Serialization.OptionalField( VersionAdded = 5 )]
        public Dictionary<string, Thumbnail> thumbnails;

        /// <summary>
        /// Video constructor, Twitch API v2
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="broadcastId">Broadcast ID</param>
        /// <param name="status">Status</param>
        /// <param name="id">ID</param>
        /// <param name="recordedAt">Recording date</param>
        /// <param name="game">Game, can be null</param>
        /// <param name="length">Length in seconds</param>
        /// <param name="preview">Link to preview image</param>
        /// <param name="url">Link to video</param>
        /// <param name="embed">Embed string</param>
        /// <param name="views">Number of views</param>
        /// <param name="broadcastType">Broadcast type</param>
        /// <param name="miniChannelO">Mini channel JSON object</param>
        /// <param name="baseLinksO">Base links JSON object</param>
        public Video(string title,
            string description,
            long broadcastId,
            string status,
            string id,
            string recordedAt,
            string game,
            long length,
            string preview,
            string url,
            string embed,
            long views,
            string broadcastType,
            JObject miniChannelO,
            JObject baseLinksO) : base(baseLinksO)
        {
            this.version = Twixel.APIVersion.v2;
            this.title = title;
            this.description = description;
            this.broadcastId = broadcastId;
            this.status = status;
            this.id = id;
            this.recordedAt = DateTime.Parse(recordedAt);
            this.game = game;
            this.length = length;
            if (!string.IsNullOrEmpty(preview))
            {
                this.preview = new Uri(preview);
            }
            this.url = new Uri(url);
            this.embed = embed;
            this.views = views;
            this.broadcastType = broadcastType;
            this.channel = LoadChannel(miniChannelO);
        }

        /// <summary>
        /// Video constructor, Twitch API v3
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="broadcastId">Broadcast ID</param>
        /// <param name="status">Status</param>
        /// <param name="tagList">Comma seperated string of tags</param>
        /// <param name="id">ID</param>
        /// <param name="recordedAt">Recording date</param>
        /// <param name="game">Game, can be null</param>
        /// <param name="length">Length in seconds</param>
        /// <param name="preview">Link to preview image</param>
        /// <param name="url">Link to video</param>
        /// <param name="views">Number of views</param>
        /// <param name="fps">FPS JSON object</param>
        /// <param name="resolutions">Resolutions JSON object</param>
        /// <param name="broadcastType">Broadcast type</param>
        /// <param name="miniChannelO">Mini channel JSON object</param>
        /// <param name="baseLinksO">Base links JSON object</param>
        public Video(string title,
            string description,
            long broadcastId,
            string status,
            string tagList,
            string id,
            string recordedAt,
            string game,
            long length,
            string preview,
            string url,
            long views,
            string fps,
            string resolutions,
            string broadcastType,
            JObject miniChannelO,
            JObject baseLinksO) : base(baseLinksO)
        {
            this.version = Twixel.APIVersion.v3;
            this.title = title;
            this.description = description;
            this.broadcastId = broadcastId;
            this.status = status;
            this.tagList = tagList;
            this.id = id;
            this.recordedAt = DateTime.Parse(recordedAt);
            this.game = game;
            this.length = length;
            if (!string.IsNullOrEmpty(preview))
            {
                this.preview = new Uri(preview);
            }
            this.url = new Uri(url);
            this.views = views;
            if (!string.IsNullOrEmpty(fps))
            {
                this.fps = LoadFps(fps);
            }
            if (!string.IsNullOrEmpty(resolutions))
            {
                this.resolutions = LoadResolutions(resolutions);
            }
            this.broadcastType = broadcastType;
            this.channel = LoadChannel(miniChannelO);
        }

        /// <summary>
        /// Video constructor, Twitch API v5
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="broadcastId">Broadcast ID</param>
        /// <param name="status">Status</param>
        /// <param name="tagList">Comma seperated string of tags</param>
        /// <param name="id">ID</param>
        /// <param name="game">Game, can be null</param>
        /// <param name="length">Length in seconds</param>
        /// <param name="preview">Link to preview image</param>
        /// <param name="url">Link to video</param>
        /// <param name="views">Number of views</param>
        /// <param name="fps">FPS JSON object</param>
        /// <param name="resolutions">Resolutions JSON object</param>
        /// <param name="broadcastType">Broadcast type</param>
        /// <param name="thumbnails">Thumbnails</param>
        /// <param name="language">Video language</param>
        /// <param name="viewable">Video viewability</param>
        /// <param name="miniChannelO">Mini channel JSON object</param>
        /// <param name="baseLinksO">Base links JSON object</param>
        public Video(string title,
            string description,
            long broadcastId,
            string status,
            string tagList,
            string id,
            string game,
            long length,
            string preview,
            string url,
            long views,
            string fps,
            string resolutions,
            string broadcastType,
            string thumbnails,
            string language,
            string viewable,
            JObject miniChannelO,
            JObject baseLinksO) : base(baseLinksO)
        {
            this.version = Twixel.APIVersion.v5;
            this.title = title;
            this.description = description;
            this.broadcastId = broadcastId;
            this.status = status;
            this.tagList = tagList;
            this.id = id;
            this.game = game;
            this.length = length;
            if (!string.IsNullOrEmpty(preview))
            {
                previewv5 = LoadPreviews(preview);
            }
            if (!string.IsNullOrEmpty(thumbnails))
            {
                this.thumbnails = LoadThumbnails(thumbnails);
            }
            this.url = new Uri(url);
            this.views = views;
            if (!string.IsNullOrEmpty(fps))
            {
                this.fps = LoadFps(fps);
            }
            if (!string.IsNullOrEmpty(resolutions))
            {
                this.resolutions = LoadResolutions(resolutions);
            }
            this.broadcastType = broadcastType;
            this.language = language;
            this.viewable = viewable;
            this.channel = LoadChannel(miniChannelO);
        }

        private Dictionary<string, double> LoadFps(string s)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, double>>(s);
        }

        private Dictionary<string, Uri> LoadPreviews(string s)
        {
            var tmp = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
            Dictionary<string, Uri> previews = new Dictionary<string, Uri>();
            foreach (var preview in tmp)
            {
                previews.Add(preview.Key, new Uri(preview.Value));
            }
            return previews;
        }

        private Dictionary<string, Thumbnail> LoadThumbnails(string s)
        {
            Dictionary<string, JArray> tmp = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(s);
            Dictionary<string, Thumbnail> thumbnails = new Dictionary<string, Thumbnail>();
            foreach (var thumbnail in tmp)
            {
                thumbnails.Add(thumbnail.Key, new Thumbnail((string)thumbnail.Value[0]["url"],
                    (string)thumbnail.Value[0]["type"]));
            }
            return thumbnails;
        }

        private Dictionary<string, Resolution> LoadResolutions(string s)
        {
            Dictionary<string, string> tmp = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
            Dictionary<string, Resolution> resolutions = new Dictionary<string, Resolution>();
            foreach (var res in tmp)
            {
                if (res.Value.Contains("x"))
                {
                    string[] splitRes = res.Value.Split('x');
                    resolutions.Add(res.Key,
                        new Resolution(int.Parse(splitRes[0]),
                        int.Parse(splitRes[1])));
                }
            }
            return resolutions;
        }
        
        private Dictionary<string, string> LoadChannel(JObject o)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(o.ToString());
        }
    }
}
