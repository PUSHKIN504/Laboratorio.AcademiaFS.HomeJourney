import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  // Arreglo con tus rutas
  routes = [
    { path: 'testpage', label: 'Test Page' },
    { path: 'anotherpage', label: 'Another Page' },
    { path: 'yetanotherpage', label: 'Yet Another Page' }
  ];

  constructor(public router: Router) {}
}
