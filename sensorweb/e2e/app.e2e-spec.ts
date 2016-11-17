import { SensorwebPage } from './app.po';

describe('sensorweb App', function() {
  let page: SensorwebPage;

  beforeEach(() => {
    page = new SensorwebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
