import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ProductDetailService } from 'src/app/service/product-detail.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Product } from 'src/app/model/product.model';

@Component({
  selector: 'app-product-register',
  templateUrl: './product-register.component.html',
  styleUrls: ['./product-register.component.css']
})
export class ProductRegisterComponent implements OnInit {
  @Output() registered: EventEmitter<any> = new EventEmitter();

  constructor(public service: ProductDetailService, private toastr: ToastrService) { }

  public list: Observable<Product[]>;
  // refreshProducts$ = new BehaviorSubject<boolean>(true);

  ngOnInit(): void {
    // this.list = this.refreshProducts$.pipe(switchMap(_ => this.service.getProductList()));
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.ProductId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.addProductDetail().subscribe(
      res => {
        this.resetForm(form);
        this.registered.emit(true);
        // this.refreshProducts$.next(true);
        this.toastr.success('Save successfully', 'Product Register');
      }
    )
  }
  updateRecord(form: NgForm) {
    this.service.updateProductDetail().subscribe(
      res => {
        this.resetForm(form);
        this.registered.emit(true);
        this.toastr.info('Updated successfully', 'Payment Detail Register')
      },
      err => { console.log(err); }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Product();
  }
}
