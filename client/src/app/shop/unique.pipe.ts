// unique.pipe.ts

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'unique'
})
export class UniquePipe implements PipeTransform {

  transform(value: any[], key: string): any[] {
    if (!Array.isArray(value) || value.length === 0) {
      return [];
    }

    const uniqueValues = [...new Set(value.map(item => item[key]))];
    return uniqueValues.map(value => {
      return { [key]: value };
    });
  }
}
