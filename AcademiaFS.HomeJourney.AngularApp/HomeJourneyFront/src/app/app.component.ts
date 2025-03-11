import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { DirectivasComponent } from './views/directives/directivas.component'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, SidebarComponent],
  templateUrl: './app.component.html'
})
export class AppComponent {

}
