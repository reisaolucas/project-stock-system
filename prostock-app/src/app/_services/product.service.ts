import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../_models/Product';

@Injectable({
  providedIn: 'root'
})

/*Service created to provide reusability to the HTTP Requests*/

export class ProductService {
  baseUrl = 'http://localhost:5000/api/product';

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  postProduct(product: Product) {
    return this.http.post(this.baseUrl, product);
  }

  putProduct(product: Product) {
    return this.http.put(`${this.baseUrl}/${product.productId}`, product);
  }
}
