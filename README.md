# entity-to-csv
A generic C# entity collection to CSV converter/serializer.

RFC4180 compitable (https://tools.ietf.org/html/rfc4180) .

This project is created using Visual Studio 2017 and .NETFramework Version v4.6.1 is used.

"EntityToCsvSerializer.cs" is the main file that converter C# Entity to CSV. 

It has only one public static generic method named "SerializeToDelimitedText<T>" that takes C# EntityCollection with an optonal "delimiter" [uses comma(',') as default] and return the CSV string.

#Example:
	
	var CsvString = EntityToCsvSerializer.SerializeToDelimitedText(PassyourEntityCollection);
	
	
For detail, please check the "EntityToCsvSerializerTests.cs" file under "EntityToCsvTests" project.

	

