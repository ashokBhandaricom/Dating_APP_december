import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_Service/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter<boolean>() ; 
  model : any = {}  

  ngOnInit(): void {
  }
constructor(private accountService : AccountService , private toastr: ToastrService){

}

register() {
 this.accountService.register(this.model).subscribe({
  next : () => {
   
    this.cancel() ; 
  },
  error : error =>  {
    this.toastr.error(error.error.errors.Password[0]);
    console.log(error) ; 

  }
 })
}

cancel(){
 //console.log("this is cancelled ") ; 
 this.cancelRegister.emit(false) ; 
}
}
