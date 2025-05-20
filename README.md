Prerequisites
.NET 8 SDK
Visual Studio 2022+ or Visual Studio Code


📚 API Endpoints
🔍 Get All Products
GET /api/product/getAllProducts
Query Parameters:
name (optional): Filter by product name
page (optional): Page number (default is 1)
pageSize (optional): Page size (default is 10)

Response:
Returns a list of Product objects.

Create Product
POST /api/product/createProduct
Body (JSON):
json
CopyEdit
{
  "name": "Sample Product",
  "price": 100,
  "description": "Short description",
  "category": "Electronics"
}

Replace with your actual ProductCreateRequest fields.
Response:
Returns the created product data.

Delete Product
DELETE /api/product/deleteProduct/{id}
Route Parameter:
id: ID of the product to delete


Response:
Returns true if deletion succeeded, false otherwise.


 


