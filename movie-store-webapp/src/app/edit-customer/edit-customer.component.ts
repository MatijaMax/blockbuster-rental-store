import { Component } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CustomerClient, Customer} from 'src/app/api/api-reference';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent {
  customerId : string ='';
  customerForm: FormGroup;
  private subscriptions: Subscription[] = [];
  constructor(public dialogRef: MatDialogRef<EditCustomerComponent>,private customerClient : CustomerClient,  private router: Router,    private route: ActivatedRoute, private fb: FormBuilder)
  {
    
    this.customerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    })
  }

  onSubmit() {
    if (this.customerForm.valid) {
      const formValues = this.customerForm.value;
      const email = formValues.email;
      console.log(this.customerId);
      console.log(email);
      const subscription = this.customerClient.updateCustomer(this.customerId, email)
      .subscribe(
        () => {
          console.log('Movie updated successfully.');
          this.router.navigate(['/customers']);
        },
        (error) => {
          console.error('Error updating movie:', error);
        }
      );

    this.subscriptions.push(subscription);

      this.dialogRef.close();
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
} 