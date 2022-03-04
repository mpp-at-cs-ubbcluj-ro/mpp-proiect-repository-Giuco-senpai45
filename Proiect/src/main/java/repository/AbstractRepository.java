package repository;

import model.Identifiable;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;

public class AbstractRepository <T extends Identifiable<ID>, ID> implements Repository<T, ID>{
    protected Map<ID,T> elem;
    private final static Logger log= LogManager.getLogger();

    public AbstractRepository(){
        elem= new HashMap<>();
    }

    @Override
    public void add(T el) {
        log.traceEntry(" parameters {}",el);
        if(elem.containsKey(el.getID()))
        {
            throw log.throwing(new RuntimeException("Element already exists!!!"));
        }
        else
            elem.put(el.getID(),el);
         log.traceExit();
    }

    @Override
    public void delete(T el) {
        log.traceEntry("{}",el);
        if(elem.containsKey(el.getID()))
            elem.remove(el.getID());
        log.traceExit();
    }

    @Override
    public void update(T el, ID id) {
        log.traceEntry("{}, {}",el,id );
        if(elem.containsKey(id))
            elem.put(el.getID(),el);
        else
            throw log.throwing(new RuntimeException("Element doesn’t exist"));
        log.traceExit();
    }

    @Override
    public T findById(ID id) {
        log.traceEntry("{}",id);
        if(elem.containsKey(id))
              return log.traceExit(elem.get(id));
        else {
            throw log.throwing(new RuntimeException("Element doesn't exist"));
        }
    }

    @Override
    public Iterable<T> findAll() {
        return elem.values();
    }

    @Override
    public Collection<T> getAll() {
        return elem.values();
    }
}
