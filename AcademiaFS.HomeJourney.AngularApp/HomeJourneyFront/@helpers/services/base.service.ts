import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { HttpErrorResponseExtends } from '../responses'; 
import { MatSnackBar } from '@angular/material/snack-bar';

export interface IParam {
  name: string;
  value: any;
  type: 'params' | 'headers';
}

export abstract class BaseService {
  params = { 
    params: new HttpParams(), 
    headers: new HttpHeaders({ 'content-type': 'application/json' }) 
  };

  httpErrorResponseExtends: HttpErrorResponseExtends;

  constructor(
    protected httpClient: HttpClient,
    private api: string,
    private callback: Function,
    private snackBar: MatSnackBar
  ) {
    // Se crea la instancia de HttpErrorResponseExtends con MatSnackBar y el callback
    this.httpErrorResponseExtends = new HttpErrorResponseExtends(this.snackBar, this.callback);
  }

  protected setParam(param: IParam): void {
    if (param.type === 'headers')
      this.params.headers = this.params.headers.set(param.name, param.value);
    else
      this.params.params = this.params.params.set(param.name, param.value);
  }

  protected removeParam(param: IParam): void {
    if (param.type === 'headers')
      this.params.headers = this.params.headers.delete(param.name, param.value);
    else
      this.params.params = this.params.params.delete(param.name, param.value);
  }

  protected $get<entity>(url: string): Observable<entity> {
    return this.httpClient.get<entity>(url, this.params);
  }

  protected $post<entity>(url: string, body: string | FormData): Observable<entity> {
    return this.httpClient.post<entity>(url, body, this.params);
  }

  protected $put<entity>(url: string, body: string | FormData): Observable<entity> {
    return this.httpClient.put<entity>(url, body, this.params);
  }

  protected $patch<entity>(url: string, body?: string | FormData): Observable<entity> {
    return this.httpClient.patch<entity>(url, body, this.params);
  }

  public get<T>(url: string, params?: IParam[]): Promise<T> {
    this.params.params = new HttpParams();
    if (params) this.addParams(params);
    return this.response<T>(this.$get<T>(`${this.api}/${url}`));
  }

  public post<T>(url: string, body: string | FormData, params?: IParam[]): Promise<T> {
    this.params.params = new HttpParams();
    if (params) this.addParams(params);
    return this.response<T>(this.$post<T>(`${this.api}/${url}`, body));
  }

  public put<T>(url: string, body: string | FormData, params?: IParam[]): Promise<T> {
    this.params.params = new HttpParams();
    if (params) this.addParams(params);
    return this.response<T>(this.$put<T>(`${this.api}/${url}`, body));
  }

  public patch<T>(url: string, body?: string | FormData, params?: IParam[]): Promise<T> {
    this.params.params = new HttpParams();
    if (params) this.addParams(params);
    return this.response<T>(this.$patch<T>(`${this.api}/${url}`, body));
  }

  private response<T>($observer: Observable<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      lastValueFrom($observer)
        .then(response => resolve(response))
        .catch(error => reject(this.httpErrorResponseExtends.customError(error)))
        .finally(() => this.params.params = new HttpParams());
    });
  }

  public addParams(params: IParam | IParam[]): void {
    this.params.params = new HttpParams();
    if (params instanceof Array) {
      params.forEach(x => this.setParam(x));
      return;
    }
    this.setParam(params);
  }

  public removeParams(param: IParam | IParam[]): void {
    if (param instanceof Array) {
      param.forEach(x => this.removeParam(x));
      return;
    }
    this.removeParam(param);
  }

  public removeContentType(value: string): void {
    this.removeParams({ name: 'content-type', value, type: 'headers' });
  }

  public addContentType(value: string): void {
    this.addParams([{ name: 'content-type', value, type: 'headers' }]);
  }

  public getHeadersForFile(headerSinContentType?: boolean) {
    let header: HttpHeaders;
    header = new HttpHeaders().set('Accept', 'application/json');
    return { headers: header };
  }
}
