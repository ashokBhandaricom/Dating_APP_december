import { Component, OnInit } from '@angular/core';
import { AccountService } from './_Service/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  /**
   *
   */
  users : any ; 
  constructor( private accountService : AccountService) {
   
    
  }
  ngOnInit(): void {
  
    this.setCurrentUser()
  }
  title = 'My dating app ';


  setCurrentUser(){
    const userString   = localStorage.getItem('user') ; 
    if(!userString) return ; 
    if( userString) {
      const user =  JSON.parse(userString);
      this.accountService.setCurrentUser(user) ; 
    }
  }



  }


