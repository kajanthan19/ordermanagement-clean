import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductItem } from '../product-item/product-item';

@Injectable({
  providedIn: 'root'
})
export class ProductItemService {

  public apiurl = environment.apiUrl + 'ProductItem';
  constructor(public http: HttpClient) { }

  addProductItem(data: any): Observable<ProductItem> {
    const url = this.apiurl;
    return this.http.post<ProductItem>(url, data);
  }

  getProductItemList(): Observable<ProductItem[]>{
    const url = this.apiurl;
    return this.http.get<ProductItem[]>(url);
  }

  updateProductItem(id: number,data: ProductItem): Observable<ProductItem> {
    const url = this.apiurl + "/" + id;
    return this.http.put<ProductItem>(url, data);
  }

  deleteProductItem(id: number): Observable<boolean> {
    const url = this.apiurl + "/" + id;
    return this.http.delete<boolean>(url);
  }
}
