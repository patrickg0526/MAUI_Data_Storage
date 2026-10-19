# MAUI_Data_Storage

A .NET MAUI inventory tracker that saves items to a local SQLite database.

- `Models/Item.cs`: Item table (Id, unique ItemId, ItemName, ItemDescription, DateAdded)
- `DataAccess/ItemData.cs`: DAL with get-all, get-by-ItemId, save, and delete methods
- `MainPage`: data entry form with required-field and duplicate-ID validation, plus a list of all stored items
