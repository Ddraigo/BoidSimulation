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



CÁC HÀM CÓ SẴN TRONG UNITY ĐƯỢC SỬ DỤNG:
1. Trong BoidMovement:
    + Vector2.Lerp(Vector2 a, Vector2 b, float t) trong đó trong đó a b là vecto điểm đầu tiên và kết thúc, t là tỉ lệ hoặc thời gian chuyển động giữa a và b

    Ứng dụng: tạo chuyển động mượt mà, hiệu ứng chuyển đổi, thay đổi tôc độ. Ở đây hàm được dùng cho mục đích cụ thể là điều hướng và vận tốc di chuyển cho cá một cách mượt mà tự nhiên.

    + Vector2.Dot(vector a, vector b): tính tích vô hướng 2 vector

    + Mathf.Cos(float f): trả về cos của một góc

    




