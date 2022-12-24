import { Component, Inject, OnInit } from '@angular/core';
import { ApiService } from '../services/api/api.service';
import { LoaderService } from '../services/loader.service';

@Component({
  selector: 'app-muro',
  templateUrl: './muro.component.html',
  providers: [ApiService]
})
export class MuroComponent implements OnInit {
  public page_title: string = "";
  public status: string = '';

  constructor(private _apiService: ApiService, private _ls: LoaderService) { }

  ngOnInit(): void {
    this._ls.signal.emit(true);
    this._apiService.Data().subscribe(
      response => {
        this.status = 'success';
        this.page_title = response[0].organization;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);
      }
    );
  }

}
