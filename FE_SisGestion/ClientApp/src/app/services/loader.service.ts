import {Injectable,Output,EventEmitter} from '@angular/core';


@Injectable({
  providedIn:'root'
})
export class LoaderService {
  @Output() signal: EventEmitter<any> = new EventEmitter();

   constructor() {}

   change(value:boolean) {
     this.signal.emit(value);
   }

} 