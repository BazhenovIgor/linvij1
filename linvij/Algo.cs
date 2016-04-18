using System;
using System.Text;

namespace Algo
{
	class Vijenera
	{
		public enum Option
		{
			First,
			Second
		}

		private const string abc = "abcdefghijklmnopqrstuvwxyzабвгдежзийклмнопрстуфхцчшщьыъэюя.;,\t:(){}[]/*";

		public string Encryption(string originalText, string secretKey, Option option)
		{
			StringBuilder result = new StringBuilder();

			var abcLength = abc.Length;
			var lowerOriginalText = originalText.ToLower();
			var lowerSecretKey = secretKey.ToLower();

			var newSecretKey = GenerateSecretKeyRelativeToInputString(lowerOriginalText, lowerSecretKey, option);

			//Console.WriteLine("New secret key: {0}", newSecretKey);

			for(var i = 0; i < lowerOriginalText.Length; i++)
			{
				if (lowerOriginalText[i] == ' ')
				{
					result.Append(lowerOriginalText[i]);
					continue;
				}

				var p = GetIndexRelativeToABC(lowerOriginalText[i]);
				var k = GetIndexRelativeToABC(newSecretKey[i]);
				var charIndex = (p + k) % abcLength;
				result.Append(GetCharFromABCByIndex(charIndex));
			}

			return result.ToString();
		}

		public string Decryption(string encryptingText, string secretKey, Option option)
		{
			StringBuilder result = new StringBuilder();

			var abcLength = abc.Length;
			var lowerEncryptingText = encryptingText.ToLower();
			var lowerSecretKey = secretKey.ToLower();

			var newSecretKey = GenerateSecretKeyRelativeToInputString(lowerEncryptingText, lowerSecretKey, option);
			for (var i = 0; i < lowerEncryptingText.Length; i++)
			{
				if (lowerEncryptingText[i] == ' ')
				{
					result.Append(lowerEncryptingText[i]);
					continue;
				}

				var c = GetIndexRelativeToABC(lowerEncryptingText[i]);
				var k = GetIndexRelativeToABC(newSecretKey[i]);
				var charIndex = (c - k + abcLength) % abcLength;
				result.Append(GetCharFromABCByIndex(charIndex));
			}

			return result.ToString();

		}

		private int GetIndexRelativeToABC(char inputChar)
		{
			return abc.IndexOf(inputChar);
		}

		private char GetCharFromABCByIndex(int index)
		{
			if (index >= abc.Length) throw new ArgumentOutOfRangeException("index");
			return abc[index];
		}

		private string GenerateSecretKeyRelativeToInputString(string inputString, string secretKey, Option option)
		{
			StringBuilder result = new StringBuilder();

			var inputStringWithOutSpace = inputString.Replace(" ", "");

			for (var i = 0; i < inputString.Length; i++)
			{
				switch(option)
				{
					case Option.First:
					var index = i >= secretKey.Length ? i % secretKey.Length : i;
					result.Append(secretKey[index]);
					break;
					case Option.Second:
					if (i >= secretKey.Length)
					{
						var idx = (i - secretKey.Length) % inputStringWithOutSpace.Length;
						result.Append(inputStringWithOutSpace[idx]);
					}
					else
					{
						result.Append(secretKey[i]);
					}
					break;
				}
			}
			return result.ToString();
		}
	}
}