# GenericRepository
Async task base entity framework core generic repository

This library is a Generic Repository implementation for EF Core ORM which will remove developers' pain to write repository layer for each .NET Core and .NET project.


### Free raw SQL support:
            List<string> search = new List<string>() { "Hossein", "Fathi" };
            string sqlQuery = "Select * from Customer Where FName LIKE @p0 + '%' and LName LIKE @p1 + '%'";
            List<Customer> items = await _repository .GetFromRawSqlAsync<Customer>(sqlQuery, search);

### Pagination Support:
            var filter = new PaginationFilter<Customer>();
            filter.Conditions.Add(e => e.FName.Contains("Ho"));
            filter.Conditions.Add(e => e.LName.Contains("Fa"));
            filter.PageIndex = 1;
            filter.PageSize = 10;
            PaginatedList<Customer> paginatedList = await _repository.GetListAsync(filter);


            
### This library includes following notable features:

1. This library can be run on any .NET Core or .NET application which has .NET Core 3.1, .NET Standard 2.1 and .NET 5.0 support.

2. Itâ€™s providing the Generic Repository with database transaction support.

3. It has all the required methods to query your data in whatever way you want without getting IQueryable<T> from the repository.

4. It also has **`Filter<T>`** pattern support so that you can build your query dynamically i.e. differed query building.

5. It also has database level projection support for your query.

6. It also has support to run raw SQL command against your relational database.

7. It also has support to choose whether you would like to track your query entity/entities or not.

8. It also has support to reset your EF Core DbContext state whenever you really needed.

9. Most importantly, it has full Unit Testing support.

11. Pagination support.

13. Free raw SQL query support both for complex type and primitive types.
