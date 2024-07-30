Thuật toán Boid viết tắt của "bird-oid object": là một thuật toán mô phỏng lại hành vi thực tế của một quần thể chim. Thực tế cũng có thể áp dụng cho các đàn cá, dê, cừu, vv. 

Mỗi cá thể trong đàn sẽ tuân theo 3 nguyên tắc đơn giản: 

1. Separate (tách): đây là hàng vi một cá thể sẽ có xu hướng lèo lái tránh các thể khác khi chúng nhận thấy cá thể đó tiến vào phạm vi
"cần tránh". Hành vi này có thể hiểu là để tránh va chạm giữa các cá thể trong quá trình di chuyển.


2. Alignment (điều hướng): cá thể có xu hướng điều chỉnh vận tốc di chuyển phù hợp với các cá thể khác nằm trong phạm vi nó có thể nhìn thấy


3. Cohesion (gắn kết/ tập hợp): cá thể sẽ luôn hướng vào trung tâm của đàn trong phạm vi nó nhìn thấy


SCRIPTABLE OBJECT:
    Scriptable Object là một class có thể sử dụng để tạo các đối tượng dữ liệu không phụ thuộc vào các GameObject trong scene. Điều này giúp việc quản lý và tái sử dụng dữ liệu trở nên dễ dàng hơn. Sử dụng Scriptable Object có thể giúp tiết kiệm bộ nhớ và công sức. Chẳng hạn như trong việc tạo các prefab / object và đều được gắn cho cùng một script nào đó, vậy khi khởi tạo thì các object thì dữ liệu này cũng được tạo tựa như các bản sao từ đó sẽ tốn bộ nhớ. Vậy nên Scriptable Object đóng vai trò là nơi chứa dữ liệu mà các object có thể truy xuất thông qua việc tham chiếu đến nó.

    ỨNG DỤNG TRONG BOID SIMULATION:
    + Trong project mô phỏng lại tập tính bầy đàn của một đàn cá, việc sử dụng Scriptable Object tạo ra một danh sách boid giúp dễ quản lý, kiểm soát, truy cập dữ liệu đàn cá trong đây, 
    + Chúng ta có thể tạo hoặc xóa đối tượng dễ dàng. 
    + class BoidMovement truy cập các đối tượng Boid thông qua danh sách đã có trên và có thể chia sẽ, tái sử dụng tài nguyên, đồng thời kiểm soát các đàn cá theo các nguyên tắc đã thiết lập mà không cần tốn công sức.

    Bên cạnh đó Scriptable Object còn cho phép chỉnh sửa các trường dữ liệu public một cách trực quan trên inspector



CÁC HÀM / THUỘC TÍNH CÓ SẴN TRONG UNITY ĐƯỢC SỬ DỤNG:

1. Instantiate(GameObject a, Vector position, Quaternion rotation): đây là một hàm giúp tạo ra các object trong runtime từ bản sao của một mẫu là GameObject / Prefab a với một vị trí và hướng xoay nào đó.

2. Vector2.Lerp(Vector2 a, Vector2 b, float t) trong đó trong đó a b là vecto điểm đầu tiên và kết thúc, t là tỉ lệ hoặc thời gian chuyển động giữa a và b. Hàm này giúp tạo chuyển động mượt mà, hiệu ứng chuyển đổi, thay đổi tôc độ. Ở đây hàm được dùng cho mục đích cụ thể là điều hướng và vận tốc di chuyển cho cá một cách mượt mà tự nhiên.

3. Quaternion.Slerp(Quaternion a, Quaternion b, float t): dùng để biễu diễn chuyển động xoay trong không gian 3D trở nên mượt mà hơn theo hình cung tròn thay vì là một chuyển động thẳng

        Sự giống nhau giữa Lerp và Slerp:
        + Đều là phép nội suy tạo ra chuỗi giá trị trung gian từ điểm đầu đến điểm cuối
        + Giúp cho các chuyển động mượt mà hơn

        Sự khác nhau:
            Lerp: Tạo ra chuyển động theo đường thẳng mượt mà nhưng nếu áp dụng cho các chuyển động xoay sẽ gây giật, không tự nhiên
            Slerp: có thể dùng tạo ra chuyển động quay một các mượt mà kể cả trong không gian 3D


4. Vector2.Dot(vector a, vector b): tính tích vô hướng 2 vector

5. Mathf.Cos(float f): trả về cos của một góc

6. OnDrawGizmosSelected(): Hàm của Unity để vẽ các đường và hình dạng giúp trực quan hóa nhưng không hiển thị trong màn hình Game mà chỉ được gọi khi chọn đối tượng trong Scene view của Unity Editor, giúp kiểm tra và quan sát được cụ thể các phương hướng di chuyển của boid. Trong đó có:

    + Gizmos.DrawWireSphere(Vector center, float radius): vẽ hình cầu tính từ vị trí của đối tượng với bán kính radius 
    + Gizmos.DrawLine(Vector from, Vector to): vẽ môt đường thẳng từ vị trí from đến vị trí to được chỉ định. Như trong đây là từ vị trí của boid hiện tại đến các boid khác. 

7. magnitude: trả về độ dài của vector

8. Transform:
    Đây là thuộc tính của các GameObject dùng để xác định vị trí, hướng và tỉ lệ của đối tượng thông qua các thuộc tính của nó là position, rotation và scale. Trong đó chúng ta thường làm việc nhiều với position và rotation cho phép truy xuất hay gán các vị trí / phương hướng cho đối tượng
    
9. Screen.width / Screen.height:
    Là thuộc tính trong class Screen có thể dùng để lấy kích thước của màn hình hiển thị hiện tại theo chiều dài (height) hay rộng (width) theo đơn vị pixel. 

10. Camera.main.orthographicSize: xác định kích thước của chế độ chiếu hình trực giao theo trục y (chiều dọc) của camera khi đang trong chế độ orthographic. Kích thước này được tính từ trung tâm camera đến nữa biên trên hoặc nữa biên dưới, tức bằng 1/2 của chiều cao khung nhìn camera. Chế độ orthographic giúp mọi vật thể vẫn giữ nguyên kích thước của nó mà không bị ảnh hưởng bởi khoảng cách xa gần của camera.

11. OverlapSphere và SphereCastAll: thuộc class Physics và sử dụng để kiểm tra các đối tượng trong một hình cầu

    Giống nhau: đêu có thể dùng để tạo một vùng hình cầu có khả năng tìm được các va chạm khi mà có object tiến vào trong phạm vi vùng đó

    Khác: 
    - OverlapSphere
        + Tạo ra khối cầu tĩnh, không di chuyển, không có tia
        + Có thể kiểm tra tất cả collider nào khi tiến vào phạm vi của nó mà không cần biết hướng hay di chuyển
        + Thích hợp để kiểm tra các đối tượng trong vùng tĩnh như kiểm tra xem có các boid khác nằm trong vùng nhìn của boid không
    
    - SphereCastAll: tạo ra một hình cầu dọc theo một tia (ray) và kiểm tra tất cả các Collider mà hình cầu chạm vào trên đường di chuyển của tia.
        + Có thể kiểm tra các đối tượng tiến vào phạm vi hình cầu dọc trên đường di chuyển của tia
        + Phụ thuộc vào độ dài của tia và hướng di chuyển 
        + Có thể tìm các va chạm tong khi di chuyển

    Hiệu suất:
    - OverlapSphere chỉ đơn giản kiểm tra các collider nằm trong phạm vi hình cầu

    - SphereCastAll vừa phải tính toán hướng di chuyển và vừa phải kiểm tra các collider tiến vào phạm vi nên cần tính toán nhiều => hiệu suất kém hơn

    Bên cạnh đó, trong boid simulation này việc áp dụng OverlapSphere sẽ dễ dàng hơn vì vị trí trung tâm đã được gắn vào boid nên không cần quan tâm đến nó đứng yên, nên có thể dùng OverlapSphere để tăng hiệu năng









