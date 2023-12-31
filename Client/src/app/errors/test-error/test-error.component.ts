import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
baseUrl = 'https://localhost:5000/api/' ;

constructor(private http : HttpClient) {}
validationErrors : string[] = [] ; 

ngoninit( ) : void{}
/**
 *
 */

get400Error () {
  this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
    next : response => console.log(response),
    error : error => console.log( error)
  })
}
get500Error () {
  this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
    next : response => console.log(response),
    error : error => console.log( error)
  })
} 
get401Error () {
  this.http.get(this.baseUrl + 'buggy/auth').subscribe({
    next : response => console.log(response),
    error : error => console.log( error)
  })
}

get400ValidationError () {
  this.http.post(this.baseUrl + 'buggy/error' , { }).subscribe({
    next : response => console.log(response),
    error : error => { console.log( error);
      this.validationErrors= error; } 
  })
}


}
