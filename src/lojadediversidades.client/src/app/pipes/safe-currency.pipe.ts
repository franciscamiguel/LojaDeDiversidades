import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Pipe({
  name: 'safeCurrency'
})
export class SafeCurrencyPipe implements PipeTransform {
  constructor(private currencyPipe: CurrencyPipe) {}

  transform(
    value: any,
    currencyCode: string = 'BRL',
    symbolDisplay: string | boolean = 'symbol',
    digitsInfo: string = '1.2-2',
    locale: string = 'pt-BR'
  ): string {
    if (typeof value !== 'number' || isNaN(value)) {
      return '-';
    }
    return this.currencyPipe.transform(value, currencyCode, symbolDisplay, digitsInfo, locale) || '-';
  }
}
