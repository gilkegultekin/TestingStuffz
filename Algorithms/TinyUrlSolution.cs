using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    /// <summary>
    /// Note: This is a companion problem to the System Design problem: Design TinyURL.
    /// TinyURL is a URL shortening service where you enter a URL such as https://leetcode.com/problems/design-tinyurl and it returns a short URL such as http://tinyurl.com/4e9iAk.
    /// 
    /// Design the encode and decode methods for the TinyURL service. There is no restriction on how your encode/decode algorithm should work. You just need to ensure that a URL can be encoded to a tiny URL and the tiny URL can be decoded to the original URL.
    /// </summary>
    public class TinyUrlSolution
    {
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string LowercaseChars = UppercaseChars.ToLowerInvariant();
        private const string Digits = "0123456789";
        private readonly string _alphanumericChars = $"{UppercaseChars}{LowercaseChars}{Digits}";
        private const int RandomStringLength = 6;
        private const int NumberOfTotalChars = 62;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private const string UrlPrefix = "http://tinyurl.com/";

        private readonly Dictionary<string, string> _codeUrlMapping = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _urlCodeMapping = new Dictionary<string, string>();

        // Encodes a URL to a shortened URL
        public string Encode(string longUrl)
        {
            if (_urlCodeMapping.TryGetValue(longUrl, out string encodedValue))
            {
                return encodedValue;
            }

            string randomString;
            do
            {
                randomString = GenerateRandomString();

            } while (_codeUrlMapping.ContainsKey(randomString));

            _codeUrlMapping.Add(randomString, longUrl);
            _urlCodeMapping.Add(longUrl, randomString);

            return $"{UrlPrefix}{randomString}";
        }

        // Decodes a shortened URL to its original URL.
        public string Decode(string shortUrl)
        {
            Uri uri = new Uri(shortUrl);
            string path = uri.AbsolutePath.Substring(1);

            return _codeUrlMapping.TryGetValue(path, out string longUrl) ? longUrl : string.Empty;
        }

        private string GenerateRandomString_WithLinq()
        {
            return new string(Enumerable.Repeat(_alphanumericChars, RandomStringLength)
                .Select(s => s[_random.Next(0, NumberOfTotalChars - 1)]).ToArray());
        }

        private string GenerateRandomString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < RandomStringLength; i++)
            {
                builder.Append(_alphanumericChars[_random.Next(0, NumberOfTotalChars - 1)]);
            }

            return builder.ToString();
        }
    }
}