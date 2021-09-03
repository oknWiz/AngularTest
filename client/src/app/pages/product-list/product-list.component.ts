import { Component, OnInit } from '@angular/core';
import { ProductDetailService } from 'src/app/service/product-detail.service';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/model/product.model';
import { UserAccountService } from 'src/app/service/user-account.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  public list: Product[];
  // refreshProducts$ = new BehaviorSubject<boolean>(true);

  constructor(
    public service: ProductDetailService,
    public userService: UserAccountService,
    private toastr: ToastrService) {

  }

  ngOnInit() {
    this.refreshList();
  }

  populateForm(selectedRecord: Product) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deleteProductDetail(id)
        .subscribe(
          res => {
            this.refreshList();
            this.toastr.error("Deleted successfully", 'Product Detail Register');
          },
          err => { console.log(err) }
        )
    }
  }

  public loadList(success: boolean) {
    if (success) this.refreshList();
  }

  refreshList() {
    this.service.getProductList().subscribe(res => {
      this.list = res;
    });
  }

  doLogout() {
    this.userService.logout();
  }
}
