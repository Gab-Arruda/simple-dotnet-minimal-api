var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var books = new List<Book> {
    new Book { Id = 1, Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams"},
    new Book { Id = 2, Title = "1984", Author = "George Orwell"},
    new Book { Id = 3, Title = "Ready Player One", Author = "Ernest Cline"},
    new Book { Id = 4, Title = "The Martian", Author = "Andy Weir"},
};

app.MapGet("/book", () => {
    return books;
});

app.MapGet("/book/{id}", (int id) => {
    var book = books.Find(a => a.Id == id);
    if (book is null)
        return Results.NotFound("Book not found!");
    return Results.Ok(book);
});

app.MapPost("/book", (Book book) =>{
    books.Add(book);
    return books;
});

app.MapPut("/book/{id}", (Book updateBook, int id) => {
    var book = books.Find(a => a.Id == id);
    if (book is null)
        return Results.NotFound("Book not found!");
    book.Title = updateBook.Title;
    book.Author = updateBook.Author;

    return Results.Ok(book);
});

app.MapDelete("/book/{id}", (int id) => {
    var book = books.Find(a => a.Id == id);
    if (book is null)
        return Results.NotFound("Book not found!");
    books.Remove(book);

    return Results.Ok(books);
});

app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }

}