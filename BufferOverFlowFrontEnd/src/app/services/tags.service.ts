import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TagsService implements OnInit {

  constructor(private api: ApiService) { }

  getTags () {
    return this.api.getRequest('/tags');
  }

  ngOnInit () {
  }
}
