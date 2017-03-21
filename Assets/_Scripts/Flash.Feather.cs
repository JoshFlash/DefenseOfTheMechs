using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Flash.Feather {

	class Utilities {

		public static int ConvertToInt(string input) {

			// Replace everything that is not a digit.
			string inputCleaned = Regex.Replace(input, "[^0-9]", "");

			int value = 0;

			// Tries to parse the int, returns false on failure.
			if (int.TryParse(inputCleaned, out value)) {
				// The result from parsing can be safely returned.
				return value;
			}

			return 0; // Or any other default value.
		}

	}


}
