import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiResponse } from '../enums/api-response';
import { TypeResponse } from '../enums/type-response';

export class HttpErrorResponseExtends {
  constructor(private snackBar: MatSnackBar, private callback: Function) {}

  public customError(responseError: HttpErrorResponse): void {
    let message: string = 'Ocurrió un error.';
    const { status, error } = responseError;

    if (status === 401) {
      this.callback();
      message = 'Debe de autenticarse nuevamente.';
    } else if (status === 404) {
      message = 'La ruta de la petición no existe.';
    } else if (!error) {
    } else {
      const { message: errMessage, type, isTrusted } = error;
      if (isTrusted) {
        message = 'Algo está mal con la red. Verifique su conexión e intente nuevamente.';
      } else if (typeof error !== 'object') {
        message = error;
      } else {
        message = errMessage;
        switch (type) {
          case TypeResponse.ErrorValidation:
          case TypeResponse.Warning:
            break;
          case TypeResponse.NotFound:
            break;
        }
      }
    }
    this.snackBar.open(message, 'Cerrar', { duration: 3000 });
  }

  public customOk<T>(response: ApiResponse): T | void {
    const { message, data } = response;
    if (Array.isArray(data) || typeof data === 'object') {
      return data as T;
    }
    const okMessage = message ? message : 'Petición realizada exitosamente.';
    this.snackBar.open(okMessage, 'Cerrar', { duration: 3000 });
  }
}
