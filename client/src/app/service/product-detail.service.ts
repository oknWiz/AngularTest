import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { Product } from '../model/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductDetailService {

  constructor(private http: HttpClient) { }

  readonly baseUrl = 'http://localhost:24373/api/Product'
  formData: Product = new Product();

  addProductDetail() {
    return this.http.post(`${this.baseUrl}/AddProduct/`, this.formData);
  }

  updateProductDetail() {
    return this.http.post(`${this.baseUrl}/UpdateProduct/`, this.formData);
  }

  deleteProductDetail(id: number) {
    return this.http.get(`${this.baseUrl}/DeleteProduct/${id}`);
  }

  getProductList(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProducts`);
  }
}
