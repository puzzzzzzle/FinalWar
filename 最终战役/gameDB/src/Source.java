import java.sql.*;

/**
 * Created by zhangjiakai on 2017/7/12.
 */
public class Source {
    static String username;
    static String pwd;
    static String urlString;

    static {
        try {
            Class.forName("com.mysql.jdbc.Driver");
            username = "root";
            pwd = "123456";
            urlString = "jdbc:mysql://localhost:3306/tao?useUnicode=true&characterEncoding=utf-8&useSSL=false";
        } catch (ClassNotFoundException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public static Connection getConnection() throws Exception {               //获取一个连接

        Connection connection = DriverManager.getConnection(urlString,
                username, pwd);
        return connection;
    }

    public static void closeDb(Connection con, PreparedStatement st, ResultSet rs) {    //资源释放
        if (rs != null) {
            try {
                rs.close();
            } catch (SQLException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            } finally {
                if (st != null) {
                    try {
                        st.close();
                    } catch (SQLException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } finally {
                        if (con != null) {
                            try {
                                con.close();
                            } catch (SQLException e) {
                                // TODO Auto-generated catch block
                                e.printStackTrace();
                            }
                        }
                    }
                }
            }
        }
    }
}
