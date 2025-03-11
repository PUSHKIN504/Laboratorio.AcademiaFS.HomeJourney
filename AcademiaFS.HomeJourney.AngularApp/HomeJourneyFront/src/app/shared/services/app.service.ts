import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseService } from '../../../../@helpers/services/base.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppService extends BaseService {
  constructor(protected override httpClient: HttpClient, snackBar: MatSnackBar) {
    super(httpClient, environment.appInfo.apis.homejourney, () => {}, snackBar);
  }
}
