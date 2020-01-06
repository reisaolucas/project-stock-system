import { Component, OnInit } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { Product } from '../_models/Product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  searchElement: string;

  get searchList(): string {
    return this.searchElement;
  }
  set searchList(value: string) {
    this.searchElement = value;
    this.foundProducts = this.searchList ? this.findProducts(this.searchList) : this.products;
  }

  foundProducts: any = [];

  products: Product[];

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getAllProducts();
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
    }, error => {
      console.log(error);
    });
  }


}
