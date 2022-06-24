import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Person } from '../person/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  public apipersonurl = environment.apiUrl + 'Person';
  constructor(public http: HttpClient) { }

  addPerson(data: Person): Observable<Person> {
    const url = this.apipersonurl;
    return this.http.post<Person>(url, data);
  }

  getPersonList(): Observable<Person[]>{
    const url = this.apipersonurl;
    return this.http.get<Person[]>(url);
  }

  updatePerson(id: number,data: Person): Observable<Person> {
    const url = this.apipersonurl + "/" + id;
    return this.http.put<Person>(url, data);
  }

  deletePerson(id: number): Observable<boolean> {
    const url = this.apipersonurl + "/" + id;
    return this.http.delete<boolean>(url);
  }
}
