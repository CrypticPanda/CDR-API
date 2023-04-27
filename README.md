Technologies
--------
- Moq for mocking of services
- CSVHelper for the reading of CSV files

Assumptions
--------
- CSV uploaded one at a time due to only one file being supplied
- All data is UK only looking at the data formats
- Made assumption on data types

Changes
--------
- XUnit for unit tests to allow inline data
- Seperate IRepo<T> interface with virtual generic methods for add/addrange/get/update
- Logging
- Error/Exception handling and try catches
- More controller calls e.g.
	-  Cost for recipient/caller and use the currency
    -  End time for latest/earliest call of the day
- Fix naming convention in model with mapping