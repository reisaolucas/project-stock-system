import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { Product } from '../_models/Product';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  searchElement: string;
  foundProducts: any = [];
  products: Product[];
  currentProduct: Product;
  registerForm: FormGroup;
  saveMode = 'post';

  constructor(private productService: ProductService, private modalService: BsModalService) { }

  ngOnInit() {
    this.validation();
    this.getAllProducts();
  }

  get searchList(): string {
    return this.searchElement;
  }
  set searchList(value: string) {
    this.searchElement = value;
    this.foundProducts = this.searchList ? this.findProducts(this.searchList) : this.products;
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  validation() {
    this.registerForm = new FormGroup({
      name: new FormControl('', Validators.required),
      amount: new FormControl('', Validators.required),
      value: new FormControl('', Validators.required)
    });
  }

  editProduct(product: Product, template: any) {
    this.saveMode = 'put';
    this.openModal(template);
    this.currentProduct = product;
    this.registerForm.patchValue(product);
  }

  newProduct(template: any) {
    this.saveMode = 'post';
    this.openModal(template);
  }

  saveChanges(template: any) {
    if (this.registerForm.valid) {
      if (this.saveMode === 'post') {
        this.currentProduct = Object.assign({}, this.registerForm.value);
        this.productService.postProduct(this.currentProduct).subscribe((returnedProduct: Product) => {
        template.hide();
        this.getAllProducts();
        }, error => {
          console.log(error);
        });
      } else {
        this.currentProduct = Object.assign({productId: this.currentProduct.productId}, this.registerForm.value);
        this.productService.putProduct(this.currentProduct).subscribe(() => {
        template.hide();
        this.getAllProducts();
        }, error => {
          console.log(error);
        });
      }

    }
  }

  findProducts(filterBy: string): Product[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.products.filter(
      product => product.name.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getAllProducts() {
    this.productService.getAllProducts().subscribe( (productsList: Product[]) => {
      this.products = productsList;
      /*for the foundProducts to be refreshed: */
      this.foundProducts = this.products;
    }, error => {
      console.log(error);
    });
  }


}
