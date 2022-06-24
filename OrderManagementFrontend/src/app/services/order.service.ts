import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../order/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  public apiOrderurl = environment.apiUrl + 'Order';
  constructor(public http: HttpClient) { }

  addOrder(data: any): Observable<Order> {
    const url = this.apiOrderurl;
    return this.http.post<Order>(url, data);
  }

  getOrderList(email: string): Observable<Order[]>{
    const url = this.apiOrderurl + "/" + email;
    return this.http.get<Order[]>(url);
  }

  updateOrder(id: number,data: Order): Observable<Order> {
    const url = this.apiOrderurl + "/" + id;
    return this.http.put<Order>(url, data);
  }

  deleteOrder(id: number): Observable<boolean> {
    const url = this.apiOrderurl + "/" + id;
    return this.http.delete<boolean>(url);
  }
}
