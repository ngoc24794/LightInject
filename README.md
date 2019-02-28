# LightInject

Cung cấp giải pháp Dependency Injection thiết kế hướng đối tượng tuân thủ nguyên tắc Inversion of Control.

## IContainer

Giao diện `IContainer` cung cấp 2 phương thức chính là `Register` cho phép đăng ký dịch vụ và `Resolve` để nhận dịch vụ. 

### Cách đăng ký dịch vụ

#### Đăng ký theo kiểu dữ liệu

<pre><code>
// đăng ký theo kiểu dữ liệu
IServiceDemo service = container.Register&lt;IServiceDemo, ServiceDemo&gt;(); 

// đăng ký theo kiểu dữ liệu và đặt tên
IServiceDemo service = container.Register&lt;IServiceDemo, ServiceDemo&gt;("ServiceDemoName"); 
</code></pre>

#### Đăng ký thể hiện

<pre><code>
// đăng ký serviceIntance có kiểu IServiceDemo
IServiceDemo service = container.RegisterInstance&lt;IServiceDemo&gt;(serviceIntance); 

// đăng ký serviceIntance có kiểu IServiceDemo và đặt tên
IServiceDemo service = container.RegisterInstance&lt;IServiceDemo&gt;(serviceIntance, "ServiceDemoName"); 
</code></pre>

#### Đăng ký Singleton

<pre><code>
// đăng ký Singleton có kiểu IServiceDemo
IServiceDemo service = container.RegisterSingleton&lt;IServiceDemo, ServiceDemo&gt;(); 

// đăng ký Singleton có kiểu IServiceDemo và đặt tên
IServiceDemo service = container.RegisterSingleton&lt;IServiceDemo, ServiceDemo&gt;("ServiceDemoName"); 
</code></pre>

### Cách nhận dịch vụ

<pre><code>
// nhận dịch vụ theo kiểu dữ liệu
IServiceDemo service = container.Resolve&lt;IServiceDemo&gt;(); 

// nhận dịch vụ theo kiểu dữ liệu và tên
IServiceDemo service = container.Resolve&lt;IServiceDemo&gt;("ServiceDemoName"); 
</code></pre>

## IEventAggregator

Giao diện `IEventAggregator` quản lý nhóm sự kiện cung cấp khả năng giao tiếp lỏng lẻo giữa các đối tượng. Phương thức `GetEvent` trả về một sự kiện `PubSubEvent`.

Sự kiện `PubSubEvent` cung cấp 2 phương thức là `Publish` để phát sinh sự kiện và `Subscribe` để đăng ký xử lí sự kiện.

<pre><code>

IEventAggregator eventAggregator = container.Resolve&lt;IEventAggregator&gt;();

// đăng ký xử lí sự kiện DemoEvent
eventAggregator.GetEvent&lt;DemoEvent&gt;().Subscribe((message)=>{ // lệnh xử lí nào đó });

// phát sinh sự kiện DemoEvent và đẩy dữ liệu "Hello World" đi
eventAggregator.GetEvent&lt;DemoEvent&gt;().Publish("Hello World");

</code></pre>
