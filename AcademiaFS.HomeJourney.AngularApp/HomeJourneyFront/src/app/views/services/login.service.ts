import { inject } from '@angular/core';
import { AppService } from '../../shared/services/app.service';
import { UsuarioLoginRequest, UsuarioDto } from '../models/user.model';

export class AuthBaseService {
  private readonly _appService = inject(AppService);
  private prefix: string = "";

  constructor(url: string) {
    this.prefix = url;
  }

  login(request: UsuarioLoginRequest): Promise<UsuarioDto> {
    return this._appService.post(`${this.prefix}/login`, JSON.stringify(request));
  }
}
