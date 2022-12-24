import { Component, OnInit } from '@angular/core';
import {LoaderService} from '../services/loader.service';


@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html'
})
export class LoaderComponent implements OnInit{
  public active : boolean;
  
  constructor(private _ls:LoaderService) {
    this.active=false;
    
  }
  
  ngOnInit(): void {
    this._ls.signal
      .subscribe(val => {
                    this.active=val;
                  }
                );
  
  }
}
