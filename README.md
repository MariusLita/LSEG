In order to solve this problem and work with the csv files i used the CsvHelper package:

Credits: https://joshclose.github.io/CsvHelper/

The program has two classes: Program ( main class) and Stocks ( which is an ojbects with the properties of csv files )

The Program class have two functions ( firstAPI and secondAPI )

The first function returns a IEnumerable object and is reading data points from the csv files.
This function is getting a string argument with the filePath of the csv files that needs to be processed.
Since the CSV files do not had any header i was using a csvConfig to disable the header record
Created a reader object to read from the files and a new instance of csvReader based on the reader object and the config file
records object is used to read all of the entries from the csv file and get them to a list.
Created the logic to generate random startIndex from the entries and take only 30 entries starting by that point.

The second function is creating the output csv files in the outlier directory.

To run this application you will need to :

- Clone the current repo

	git clone https://github.com/MariusLita/LSEG.git

- Navigate to the project directory

	cd LSEG/ProjectFile

- Build the project

	dotnet build
	
- Run the application

	dotnet run LSEG_API.csproj